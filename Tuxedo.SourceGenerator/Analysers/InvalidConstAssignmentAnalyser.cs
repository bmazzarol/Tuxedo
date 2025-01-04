#pragma warning disable RS2008

using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator.Analysers;

/// <summary>
/// An analyser that prevents assignment of constant values that do not pass the
/// refinement predicates of the refined types
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class InvalidConstAssignmentAnalyser : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor Rule =
        new(
            RuleIdentifiers.InvalidConstAssignment,
            "Assigning compile time known values that do not pass refinement will fail",
            "{0}",
            RuleCategories.Usage,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: "Assigning compile time known values that do not pass refinement will result in runtime failure, so alter the value or the code.",
            helpLinkUri: $"https://bmazzarol.github.io/Tuxedo/rules/{RuleIdentifiers.InvalidConstAssignment}.html"
        );

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

    private static Lazy<Assembly?>? _currentAssembly;

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(compilationContext =>
        {
            if (!compilationContext.Compilation.HasRefinedTypeAttribute())
            {
                return;
            }

            // we build an in memory assembly for the compilation so we can run
            // the refinement predicates
            if (_currentAssembly is null)
            {
#pragma warning disable S2696
                _currentAssembly = new Lazy<Assembly?>(() =>
#pragma warning restore S2696
                {
                    using var ms = new MemoryStream();
                    var result = compilationContext.Compilation.Emit(
                        ms,
                        cancellationToken: compilationContext.CancellationToken
                    );

                    if (!result.Success)
                    {
                        return null;
                    }

                    ms.Seek(0, SeekOrigin.Begin);
#pragma warning disable RS1035 // we need to load it here, as we want to run code against the current state of the compilation.
                    return Assembly.Load(ms.ToArray());
#pragma warning restore RS1035
                });
            }

            if (_currentAssembly.Value is not { } analysedAssembly)
            {
                return;
            }

            compilationContext.RegisterOperationAction(
                analysisContext => Analyze(analysisContext, analysedAssembly),
                OperationKind.Invocation
            );
        });
    }

    private static readonly ConcurrentDictionary<string, MethodInfo?> TryParseDelegates =
        new(StringComparer.Ordinal);

    [SuppressMessage("Design", "MA0051:Method is too long")]
    private static void Analyze(OperationAnalysisContext ctx, Assembly assembly)
    {
        var operation = (IInvocationOperation)ctx.Operation;
        if (
            !operation.TargetMethod.IsStatic
            || operation.TargetMethod.Name is not ("Parse" or "TryParse")
            || !operation.TargetMethod.ContainingType.IsRefinedType()
        )
        {
            return;
        }

        // we try and get the constant value of the first argument
        var firstArgument = operation.Arguments[0];
        var constantValue =
            firstArgument.Value.ConstantValue.Value ?? firstArgument.ConstantValue.Value;
        if (constantValue is null)
        {
            return;
        }

        var fullName = operation.TargetMethod.ContainingType.ToDisplayString();

        try
        {
            var tryParseDelegate = TryParseDelegates.GetOrAdd(
                fullName,
                fn =>
                {
                    // we try and get the refined type out of the assembly
                    var type = assembly.GetType(fn);

                    if (type == null)
                    {
                        return null;
                    }

                    // now we get the static method and call it to find out if the
                    // constant would pass the refinement predicate
                    var methodInfo = type.GetMethod(
                        "TryParse",
                        BindingFlags.Public | BindingFlags.Static
                    );

                    if (methodInfo == null)
                    {
                        return null;
                    }

#pragma warning disable MA0026, S1135
                    return methodInfo; // TODO: use expressions to make this faster
#pragma warning restore S1135, MA0026
                }
            );

            if (tryParseDelegate is null)
            {
                return;
            }

            // out parameters
            object?[] parameters = [constantValue, null, null];
            if (tryParseDelegate.Invoke(null, parameters) is true)
            {
                return;
            }

            var diagnostic = Diagnostic.Create(
                descriptor: Rule,
                location: ctx.Operation.Syntax.GetLocation(),
                messageArgs: parameters[2]
            );

            ctx.ReportDiagnostic(diagnostic);
        }
        catch
        {
            // we just give up, we tried
        }
    }
}
