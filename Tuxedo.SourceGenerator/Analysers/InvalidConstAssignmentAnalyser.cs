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

    private static Lazy<Type?>? _refinementServiceType;

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
            if (_refinementServiceType is null)
            {
#pragma warning disable S2696
                _refinementServiceType = new Lazy<Type?>(
#pragma warning restore S2696
                    () =>
                        BuildIntoRuntimeRefinementServiceType(
                            compilationContext.Compilation,
                            compilationContext.CancellationToken
                        )
                );
            }

            if (_refinementServiceType.Value is not { } refinementServiceType)
            {
                return;
            }

            compilationContext.RegisterOperationAction(
                analysisContext => Analyze(analysisContext, refinementServiceType),
                OperationKind.Invocation
            );
        });
    }

    private static Type? BuildIntoRuntimeRefinementServiceType(
        Compilation compilation,
        CancellationToken token
    )
    {
        using var ms = new MemoryStream();
        var result = compilation.Emit(ms, cancellationToken: token);

        if (!result.Success)
        {
            return null;
        }

        ms.Seek(0, SeekOrigin.Begin);
#pragma warning disable RS1035 // we need to load it here, as we want to run code against the current state of the compilation
        var assembly = Assembly.Load(ms.ToArray());
#pragma warning restore RS1035
        return assembly.GetType("Tuxedo.RefinementService");
    }

    private static readonly ConcurrentDictionary<string, Func<object, string?>?> TryParseDelegates =
        new(StringComparer.Ordinal);

    [SuppressMessage("Design", "MA0051:Method is too long")]
    private static void Analyze(OperationAnalysisContext ctx, Type refinementServiceType)
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

        try
        {
            // build a refinement delegate and run the constant value against it
            var refinementDelegate = TryParseDelegates.GetOrAdd(
                operation.TargetMethod.ContainingType.ToDisplayString(),
                s => BuildRefinementDelegate(s, refinementServiceType)
            );

            var failureMessage = refinementDelegate?.Invoke(constantValue);

            if (failureMessage is null)
            {
                return;
            }

            var diagnostic = Diagnostic.Create(
                descriptor: Rule,
                location: ctx.Operation.Syntax.GetLocation(),
                messageArgs: failureMessage
            );

            ctx.ReportDiagnostic(diagnostic);
        }
        catch
        {
            // we just give up, we tried
        }
    }

    private static Func<object, string?>? BuildRefinementDelegate(
        string fn,
        Type refinementServiceType
    )
    {
        var methodInfo = refinementServiceType.GetMethod(
            $"TestAgainst{fn.RemoveNamespace()}",
#pragma warning disable S3011
            BindingFlags.NonPublic | BindingFlags.Static
#pragma warning restore S3011
        );

        if (methodInfo == null)
        {
            return null;
        }

        return (Func<object, string?>?)methodInfo.CreateDelegate(typeof(Func<object, string?>));
    }
}
