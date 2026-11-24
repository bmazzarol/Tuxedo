#pragma warning disable S3060
#pragma warning disable S2955
#pragma warning disable MA0006
#pragma warning disable CS1591

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace Firebreak;

/// <summary>
/// A pipeline value that is either success or located failure.
///
/// Firebreak is source-only and intentionally small: it turns generator
/// exceptions into value-equatable diagnostics so incremental Roslyn generators
/// can cache failures and report precise errors without crashing the whole
/// generator with AD0001.
/// </summary>
public abstract record Result<T>
{
    public static implicit operator Result<T>(T value) => new Ok<T>(value);

    public static implicit operator Result<T>(GenerationError error) =>
        new Err<T>(EquatableArray<GenerationError>.Create(error));

    public static implicit operator Result<T>(EquatableArray<GenerationError> errors) =>
        new Err<T>(errors);

    public abstract Result<U> Map<U>(
        Func<T, U> selector,
        Func<T, SyntaxNode>? originSelector = null,
        DiagnosticDescriptor? fallbackDescriptor = null
    );

    public abstract TResult Match<TResult>(Func<T, TResult> onOk, Func<Err<T>, TResult> onErr);

    public bool IsOk => this is Ok<T>;

    public bool IsErr => this is Err<T>;

    /// <summary>
    /// Returns the success value, or throws a located failure when the result is
    /// an Err. Use this only at trusted boundaries where a failure should be
    /// surfaced loudly.
    /// </summary>
    public T Unwrap()
    {
        if (this is Ok<T> ok)
        {
            return ok.Value;
        }

        var error = ((Err<T>)this).Errors.FirstOrDefault();
        if (error == null)
        {
            throw new InvalidOperationException("Err result has no diagnostic payload.");
        }

        throw new LocatedFailureException(error);
    }

    /// <summary>
    /// Returns the success value, or a fallback when the result is an Err.
    /// </summary>
    public T UnwrapOr(T fallback)
    {
        return this is Ok<T> ok ? ok.Value : fallback;
    }

    /// <summary>
    /// Returns the success value, or computes a fallback from the failure payload.
    /// </summary>
    public T UnwrapOrElse(Func<Err<T>, T> fallback)
    {
        return this is Ok<T> ok ? ok.Value : fallback((Err<T>)this);
    }

    /// <summary>
    /// Replaces an Err with a successful fallback Result, preserving Ok unchanged.
    /// </summary>
    public Result<T> OrElse(T fallback)
    {
        return this is Ok<T> ? this : new Ok<T>(fallback);
    }

    /// <summary>
    /// Attempts to extract the success value. Returns false without throwing for
    /// Err values.
    /// </summary>
    public bool Try(out T value)
    {
        if (this is Ok<T> ok)
        {
            value = ok.Value;
            return true;
        }

        value = default!;
        return false;
    }
}

/// <summary>
/// A successful pipeline value.
/// </summary>
public sealed record Ok<T>(T Value) : Result<T>
{
    public override TResult Match<TResult>(Func<T, TResult> onOk, Func<Err<T>, TResult> onErr)
    {
        return onOk(Value);
    }

    public override Result<U> Map<U>(
        Func<T, U> selector,
        Func<T, SyntaxNode>? originSelector = null,
        DiagnosticDescriptor? fallbackDescriptor = null
    )
    {
        try
        {
            return new Ok<U>(selector(Value));
        }
        catch (LocatedFailureException located)
        {
            return new Err<U>(EquatableArray<GenerationError>.Create(located.Error));
        }
        catch (Exception ex)
        {
            var descriptor = fallbackDescriptor ?? FirebreakDescriptors.InternalError;
            var origin = originSelector == null ? null : originSelector(Value);
            return new Err<U>(
                EquatableArray<GenerationError>.Create(
                    GenerationError.FromException(descriptor, ex, origin)
                )
            );
        }
    }
}

/// <summary>
/// A failed pipeline item carrying located diagnostics.
///
/// Err payloads are value-equatable so incremental Roslyn caching can re-use
/// the same failure data across driver runs without duplicate diagnostics.
/// </summary>
public sealed record Err<T>(EquatableArray<GenerationError> Errors) : Result<T>
{
    public override TResult Match<TResult>(Func<T, TResult> onOk, Func<Err<T>, TResult> onErr)
    {
        return onErr((Err<T>)this);
    }

    public override Result<U> Map<U>(
        Func<T, U> selector,
        Func<T, SyntaxNode>? originSelector = null,
        DiagnosticDescriptor? fallbackDescriptor = null
    )
    {
        return new Err<U>(Errors);
    }

    public ImmutableArray<Diagnostic> ToDiagnostics()
    {
        var diagnostics = new Diagnostic[Errors.Count];
        var index = 0;
        foreach (var error in Errors)
        {
            diagnostics[index++] = error.ToDiagnostic();
        }
        return diagnostics.ToImmutableArray();
    }
}

/// <summary>
/// Static helpers for composing Firebreak results.
/// </summary>
public static class FirebreakPipeline
{
    public static Result<T> Ok<T>(T value) => new Ok<T>(value);

    public static Result<T> Err<T>(GenerationError error) =>
        new Err<T>(EquatableArray<GenerationError>.Create(error));

    public static Result<T> Err<T>(IEnumerable<GenerationError> errors) =>
        new Err<T>(EquatableArray<GenerationError>.Create(errors));

    /// <summary>
    /// Creates a located failure from a descriptor and syntax node.
    /// </summary>
    public static Result<T> Fail<T>(
        DiagnosticDescriptor descriptor,
        string message,
        SyntaxNode node
    )
    {
        return new Err<T>(
            EquatableArray<GenerationError>.Create(GenerationError.At(descriptor, message, node))
        );
    }

    /// <summary>
    /// Creates a failure without a syntax-node location.
    /// </summary>
    public static Result<T> Fail<T>(DiagnosticDescriptor descriptor, string message)
    {
        return new Err<T>(
            EquatableArray<GenerationError>.Create(GenerationError.At(descriptor, message, null))
        );
    }

    /// <summary>
    /// Captures a synchronous stage as a Result. Exceptions become located Err
    /// values; LocatedFailureException preserves already-located errors.
    /// </summary>
    public static Result<T> Capture<T>(
        Func<T> action,
        SyntaxNode? origin = null,
        DiagnosticDescriptor? fallbackDescriptor = null
    )
    {
        try
        {
            return new Ok<T>(action());
        }
        catch (LocatedFailureException located)
        {
            return new Err<T>(EquatableArray<GenerationError>.Create(located.Error));
        }
        catch (Exception ex)
        {
            return new Err<T>(
                EquatableArray<GenerationError>.Create(
                    GenerationError.FromException(
                        fallbackDescriptor ?? FirebreakDescriptors.InternalError,
                        ex,
                        origin
                    )
                )
            );
        }
    }

    /// <summary>
    /// Splits results into values that succeeded and errors that failed. This is
    /// the key helper for generators where one bad input should not sink the
    /// whole output.
    /// </summary>
    public static PartitionedResult<T> Partition<T>(IEnumerable<Result<T>> results)
    {
        var values = new List<T>();
        var errors = new List<GenerationError>();

        foreach (var result in results)
        {
            if (result is Ok<T> ok)
            {
                values.Add(ok.Value);
            }
            else if (result is Err<T> error)
            {
                errors.AddRange(error.Errors);
            }
        }

        return new PartitionedResult<T>(
            values.ToImmutableArray(),
            EquatableArray<GenerationError>.Create(errors)
        );
    }

    /// <summary>
    /// Merges many same-type Results. If all succeed, the first value wins. If
    /// any fail, every collected error is preserved.
    /// </summary>
    public static Result<T> CombineMany<T>(IEnumerable<Result<T>> results)
    {
        Result<T>? firstOk = null;
        var errors = new List<GenerationError>();

        foreach (var result in results)
        {
            if (result is Ok<T> ok)
            {
                firstOk ??= ok;
            }
            else if (result is Err<T> error)
            {
                errors.AddRange(error.Errors);
            }
        }

        if (errors.Count == 0)
        {
            return firstOk
                ?? new Err<T>(
                    EquatableArray<GenerationError>.Create(
                        GenerationError.At(
                            FirebreakDescriptors.InternalError,
                            "No result supplied to CombineMany.",
                            null
                        )
                    )
                );
        }

        return new Err<T>(EquatableArray<GenerationError>.Create(errors));
    }

    /// <summary>
    /// Combines two independent lanes. Both Ok gives the pair; any Err lanes merge
    /// their diagnostics so each failing lane reports its own failures.
    /// </summary>
    public static Result<(A, B)> Zip<A, B>(Result<A> left, Result<B> right)
    {
        if (left is Ok<A> a && right is Ok<B> b)
        {
            return new Ok<(A, B)>((a.Value, b.Value));
        }

        var errors = new List<GenerationError>();
        if (left is Err<A> leftError)
        {
            errors.AddRange(leftError.Errors);
        }
        if (right is Err<B> rightError)
        {
            errors.AddRange(rightError.Errors);
        }

        return new Err<(A, B)>(EquatableArray<GenerationError>.Create(errors));
    }

    /// <summary>
    /// Merges two results of the same type. If either fails, all errors are kept.
    /// If both succeed, the left value wins.
    /// </summary>
    public static Result<T> Combine<T>(Result<T> left, Result<T> right)
    {
        if (left is Ok<T> leftOk && right is Ok<T>)
        {
            return leftOk;
        }

        var errors = new List<GenerationError>();
        if (left is Err<T> leftError)
        {
            errors.AddRange(leftError.Errors);
        }
        if (right is Err<T> rightError)
        {
            errors.AddRange(rightError.Errors);
        }

        return new Err<T>(EquatableArray<GenerationError>.Create(errors));
    }
}

/// <summary>
/// A failure that is fully describable as data.
///
/// The SyntaxTree reference is intentionally part of the payload: it is
/// reference-stable enough for incremental caching, while still allowing a
/// precise Location to be materialised when diagnostics are emitted.
/// </summary>
public sealed record GenerationError(
    DiagnosticDescriptor Descriptor,
    string Message,
    SyntaxTree? Tree,
    TextSpan Span,
    ImmutableArray<string> MessageArgs = default
)
{
    public static GenerationError At(
        DiagnosticDescriptor descriptor,
        string message,
        SyntaxNode? node
    )
    {
        return node == null
            ? new GenerationError(descriptor, message, null, default, ImmutableArray<string>.Empty)
            : new GenerationError(
                descriptor,
                message,
                node.SyntaxTree,
                node.Span,
                ImmutableArray<string>.Empty
            );
    }

    public static GenerationError At(
        DiagnosticDescriptor descriptor,
        string message,
        SyntaxNode? node,
        ImmutableArray<string> messageArgs
    )
    {
        return node == null
            ? new GenerationError(descriptor, message, null, default, messageArgs)
            : new GenerationError(descriptor, message, node.SyntaxTree, node.Span, messageArgs);
    }

    public GenerationError WithMessageArgs(params string[] messageArgs)
    {
        return new GenerationError(Descriptor, Message, Tree, Span, messageArgs.ToImmutableArray());
    }

    public static GenerationError FromException(
        DiagnosticDescriptor descriptor,
        Exception exception,
        SyntaxNode? origin
    )
    {
        return At(descriptor, exception.Message, origin);
    }

    public Diagnostic ToDiagnostic()
    {
        var location = Tree == null ? Location.None : Tree.GetLocation(Span);
        return Diagnostic.Create(
            Descriptor,
            location,
            !MessageArgs.IsDefault && MessageArgs.Length > 0 ? MessageArgs.ToArray() : Message
        );
    }
}

/// <summary>
/// Static helpers for Roslyn source-generator output callbacks.
/// </summary>
public static class FirebreakSourceOutputExtensions
{
    public static void ReportDiagnostics<T>(this SourceProductionContext context, Result<T> result)
    {
        if (result is Err<T> error)
        {
            foreach (var diagnostic in error.ToDiagnostics())
            {
                context.ReportDiagnostic(diagnostic);
            }
        }
    }

    public static void AddSourceOrReport(
        this SourceProductionContext context,
        string hintName,
        Result<string> result
    )
    {
        if (result is Ok<string> ok)
        {
            context.AddSource(hintName, SourceText.From(ok.Value ?? string.Empty, Encoding.UTF8));
            return;
        }

        if (result is Err<string> error)
        {
            foreach (var diagnostic in error.ToDiagnostics())
            {
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}

/// <summary>
/// Convenience overloads for incremental Roslyn provider pipelines.
/// </summary>
public static class IncrementalValuesProviderExtensions
{
    public static IncrementalValuesProvider<Result<U>> Select<TIn, U>(
        this IncrementalValuesProvider<Result<TIn>> source,
        Func<TIn, U> selector,
        Func<TIn, SyntaxNode>? originSelector = null,
        DiagnosticDescriptor? fallbackDescriptor = null
    )
    {
        return source.Select(
            (result, _) => result.Map(selector, originSelector, fallbackDescriptor)
        );
    }
}

/// <summary>
/// The result of splitting a collection of Result values into good values and
/// collected errors. This is the pattern needed for "one bad item does not sink
/// the good items".
/// </summary>
public sealed record PartitionedResult<T>(
    ImmutableArray<T> Values,
    EquatableArray<GenerationError> Errors
)
{
    public bool Equals(PartitionedResult<T> other)
    {
        if (other == null)
        {
            return false;
        }

        if (Values.Length != other.Values.Length)
        {
            return false;
        }

        for (var i = 0; i < Values.Length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(Values[i], other.Values[i]))
            {
                return false;
            }
        }

        return Errors.Equals(other.Errors);
    }

    public override int GetHashCode()
    {
        var hash = Errors.GetHashCode();
        foreach (var value in Values)
        {
            hash = hash * 17 + (value == null ? 0 : value.GetHashCode());
        }
        return hash;
    }
}

/// <summary>
/// Reporting helpers for diagnostic analyzers.
/// </summary>
public static class FirebreakAnalysisContextExtensions
{
    public static void Report<T>(this SyntaxNodeAnalysisContext context, Result<T> result)
    {
        if (result is Err<T> error)
        {
            foreach (var diagnostic in error.ToDiagnostics())
            {
                context.ReportDiagnostic(diagnostic);
            }
        }
    }

    public static void Report<T>(this OperationAnalysisContext context, Result<T> result)
    {
        if (result is Err<T> error)
        {
            foreach (var diagnostic in error.ToDiagnostics())
            {
                context.ReportDiagnostic(diagnostic);
            }
        }
    }

    public static void Report<T>(this CompilationAnalysisContext context, Result<T> result)
    {
        if (result is Err<T> error)
        {
            foreach (var diagnostic in error.ToDiagnostics())
            {
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}

/// <summary>
/// Safe semantic-model accessors that turn common null-forcing failures into
/// located diagnostics instead of generator crashes.
/// </summary>
public static class FirebreakSemanticHelpers
{
    public static Result<TSymbol> GetDeclaredSymbol<TSymbol>(
        SyntaxNode node,
        SemanticModel semanticModel,
        CancellationToken cancellationToken,
        DiagnosticDescriptor? fallbackDescriptor = null
    )
        where TSymbol : ISymbol
    {
        var symbol = semanticModel.GetDeclaredSymbol(node, cancellationToken);
        if (symbol is TSymbol typed)
        {
            return new Ok<TSymbol>(typed);
        }

        return new Err<TSymbol>(
            EquatableArray<GenerationError>.Create(
                GenerationError.At(
                    fallbackDescriptor ?? FirebreakDescriptors.InternalError,
                    "Could not resolve the declared symbol for the target node.",
                    node
                )
            )
        );
    }

    public static Result<ITypeSymbol> GetTypeInfo(
        SyntaxNode node,
        SemanticModel semanticModel,
        CancellationToken cancellationToken,
        DiagnosticDescriptor? fallbackDescriptor = null
    )
    {
        var typeInfo = semanticModel.GetTypeInfo(node, cancellationToken);
        if (typeInfo.Type != null)
        {
            return new Ok<ITypeSymbol>(typeInfo.Type);
        }

        return new Err<ITypeSymbol>(
            EquatableArray<GenerationError>.Create(
                GenerationError.At(
                    fallbackDescriptor ?? FirebreakDescriptors.InternalError,
                    "Could not resolve the type of the target expression.",
                    node
                )
            )
        );
    }

    public static Result<object?> GetConstantValue(
        ExpressionSyntax expression,
        SemanticModel semanticModel,
        CancellationToken cancellationToken,
        DiagnosticDescriptor? fallbackDescriptor = null
    )
    {
        var constant = semanticModel.GetConstantValue(expression, cancellationToken);
        if (constant.HasValue)
        {
            return new Ok<object?>(constant.Value);
        }

        return new Err<object?>(
            EquatableArray<GenerationError>.Create(
                GenerationError.At(
                    fallbackDescriptor ?? FirebreakDescriptors.InternalError,
                    "Expected a compile-time constant value.",
                    expression
                )
            )
        );
    }
}

/// <summary>
/// Attribute extraction helpers. These avoid the common AD0001 pattern where
/// attribute argument code uses Single/! without validating the syntax first.
/// </summary>
public static class FirebreakAttributeHelpers
{
    public static Result<AttributeSyntax> TryGetAttribute(
        SyntaxNode node,
        string attributeName,
        DiagnosticDescriptor? fallbackDescriptor = null
    )
    {
        var attribute = node.DescendantNodes()
            .OfType<AttributeSyntax>()
            .FirstOrDefault(a =>
                a.Name.ToString() == attributeName
                || a.Name.ToString().EndsWith("." + attributeName, StringComparison.Ordinal)
            );

        if (attribute == null)
        {
            return new Err<AttributeSyntax>(
                EquatableArray<GenerationError>.Create(
                    GenerationError.At(
                        fallbackDescriptor ?? FirebreakDescriptors.InternalError,
                        $"Missing attribute: {attributeName}.",
                        node
                    )
                )
            );
        }

        return new Ok<AttributeSyntax>(attribute);
    }

    public static Result<AttributeArgumentSyntax> TryGetArgument(
        AttributeSyntax attribute,
        int index,
        DiagnosticDescriptor? fallbackDescriptor = null
    )
    {
        if (attribute.ArgumentList == null)
        {
            return new Err<AttributeArgumentSyntax>(
                EquatableArray<GenerationError>.Create(
                    GenerationError.At(
                        fallbackDescriptor ?? FirebreakDescriptors.InternalError,
                        "Attribute has no arguments.",
                        attribute
                    )
                )
            );
        }

        var arguments = attribute.ArgumentList.Arguments;
        if (index < 0 || index >= arguments.Count)
        {
            return new Err<AttributeArgumentSyntax>(
                EquatableArray<GenerationError>.Create(
                    GenerationError.At(
                        fallbackDescriptor ?? FirebreakDescriptors.InternalError,
                        $"Attribute argument {index} is missing.",
                        attribute
                    )
                )
            );
        }

        return new Ok<AttributeArgumentSyntax>(arguments[index]);
    }

    public static Result<string> GetConstantStringArgument(
        AttributeSyntax attribute,
        int index,
        SemanticModel semanticModel,
        CancellationToken cancellationToken,
        DiagnosticDescriptor? fallbackDescriptor = null
    )
    {
        var argument = FirebreakAttributeHelpers.TryGetArgument(
            attribute,
            index,
            fallbackDescriptor
        );
        if (argument is Err<AttributeArgumentSyntax> error)
        {
            return new Err<string>(error.Errors);
        }

        var value = FirebreakSemanticHelpers.GetConstantValue(
            ((Ok<AttributeArgumentSyntax>)argument).Value.Expression,
            semanticModel,
            cancellationToken,
            fallbackDescriptor
        );

        if (value is Err<object?> constantError)
        {
            return new Err<string>(constantError.Errors);
        }

        var constant = ((Ok<object?>)value).Value;
        if (constant == null)
        {
            return new Err<string>(
                EquatableArray<GenerationError>.Create(
                    GenerationError.At(
                        fallbackDescriptor ?? FirebreakDescriptors.InternalError,
                        "Attribute argument constant value is null.",
                        attribute
                    )
                )
            );
        }

        return new Ok<string>(constant.ToString() ?? string.Empty);
    }
}

/// <summary>
/// Generic descriptors used when a consumer does not provide a more specific
/// fallback descriptor. Consumer-specific descriptors should normally be static
/// singletons defined by the generator project.
/// </summary>
public static class FirebreakDescriptors
{
#pragma warning disable RS2008

    public static readonly DiagnosticDescriptor InternalError = new DiagnosticDescriptor(
        "FIREBREAK999",
        "Generator pipeline error",
        "The generator caught an internal failure: {0}",
        "Firebreak",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

#pragma warning restore RS2008
}

/// <summary>
/// Value-equatable array wrapper. ImmutableArray does not provide structural
/// equality, so Err payloads need this to cache successfully.
/// </summary>
public sealed class EquatableArray<T> : IEnumerable<T>, IEquatable<EquatableArray<T>>
    where T : class
{
    private readonly ImmutableArray<T> _items;
    private readonly int _hashCode;

    public EquatableArray(ImmutableArray<T> items)
    {
        _items = items;
        var hash = 0;
        foreach (var item in items)
        {
            hash = hash * 17 + (item == null ? 0 : item.GetHashCode());
        }
        _hashCode = hash;
    }

    public static EquatableArray<T> Create(params T[] items) =>
        new EquatableArray<T>(items.ToImmutableArray());

    public static EquatableArray<T> Create(IEnumerable<T> items) =>
        new EquatableArray<T>(items.ToImmutableArray());

    public int Count => _items.Length;

    public bool Equals(EquatableArray<T> other)
    {
        if (other == null)
        {
            return false;
        }
        if (_items.Length != other._items.Length)
        {
            return false;
        }
        for (var i = 0; i < _items.Length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(_items[i], other._items[i]))
            {
                return false;
            }
        }
        return true;
    }

    public override bool Equals(object obj) => obj is EquatableArray<T> other && Equals(other);

    public override int GetHashCode() => _hashCode;

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _items)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

/// <summary>
/// Pipeline-local failure that already carries its located diagnostic payload.
/// Map surfaces it directly as Err instead of treating it as an internal bug.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Roslynator",
    "RCS1194:Implement exception constructors"
)]
public sealed class LocatedFailureException : Exception
{
    public GenerationError Error { get; }

    public LocatedFailureException(GenerationError error)
        : base(error.Message)
    {
        Error = error;
    }

    public LocatedFailureException(GenerationError error, Exception cause)
        : base(error.Message, cause)
    {
        Error = error;
    }

    public LocatedFailureException(string message)
        : base(message)
    {
        Error = new GenerationError(FirebreakDescriptors.InternalError, message, null, default);
    }

    public LocatedFailureException(string message, Exception cause)
        : base(message, cause)
    {
        Error = new GenerationError(FirebreakDescriptors.InternalError, message, null, default);
    }
}
