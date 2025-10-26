#pragma warning disable RS2008

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Tuxedo.SourceGenerator.Analysers;

/// <summary>
/// An analyser that prevents assignment of constant values that do not pass the
/// refinement predicates of the refined types
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed partial class InvalidConstAssignmentAnalyser : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor Rule = new(
        RuleIdentifiers.InvalidConstAssignment,
        "Assigning compile time known values that do not pass refinement will fail",
        "{0}",
        RuleCategories.Usage,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Assigning compile time known values that do not pass refinement will result in runtime failure, so alter the value or the code.",
        helpLinkUri: $"https://bmazzarol.github.io/Tuxedo/rules/{RuleIdentifiers.InvalidConstAssignment}.html"
    );

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

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

            _refinementService ??= new Lazy<RefinementService?>(() =>
                RefinementService.Build(
                    compilationContext.Compilation,
                    compilationContext.CancellationToken
                )
            );

            if (_refinementService.Value is not { } refinementService)
            {
                return;
            }

            compilationContext.RegisterOperationAction(
                analysisContext => AnalyzeInvocation(analysisContext, refinementService),
                OperationKind.Invocation
            );
            compilationContext.RegisterOperationAction(
                analysisContext => AnalyzeConversion(analysisContext, refinementService),
                OperationKind.Conversion
            );
        });
    }

    private static bool IsStaticParseMethod(IInvocationOperation operation)
    {
        return operation.TargetMethod is { IsStatic: true, Name: "Parse" or "TryParse" } tm
            && tm.ContainingType.IsRefinedType();
    }

    private static void AnalyzeInvocation(
        OperationAnalysisContext ctx,
        RefinementService refinementService
    )
    {
        var operation = (IInvocationOperation)ctx.Operation;
        if (!IsStaticParseMethod(operation))
        {
            return;
        }

        var firstArgument = operation.Arguments[0];
        var constantValue =
            firstArgument.Value.ConstantValue.Value ?? firstArgument.ConstantValue.Value;
        if (constantValue is null)
        {
            return;
        }

        TryTestAndReport(
            ctx,
            refinementService,
            fullTypeName: operation.TargetMethod.ContainingType.ToDisplayString(),
            constantValue
        );
    }

    private static bool IsExplicitConversionToRefinedType(IConversionOperation operation)
    {
        return operation.Conversion
                is {
                    IsImplicit: false,
                    IsUserDefined: true,
                    MethodSymbol: { ContainingType: { } ct, Name: "op_Explicit" }
                }
            && ct.IsRefinedType();
    }

    private static void AnalyzeConversion(
        OperationAnalysisContext ctx,
        RefinementService refinementService
    )
    {
        var operation = (IConversionOperation)ctx.Operation;
        if (!IsExplicitConversionToRefinedType(operation))
        {
            return;
        }

        var constantValue = operation.Operand.ConstantValue.Value;
        if (constantValue is null)
        {
            return;
        }

        TryTestAndReport(
            ctx,
            refinementService,
            fullTypeName: operation.Conversion.MethodSymbol!.ContainingType.ToDisplayString(),
            constantValue
        );
    }

    private static void TryTestAndReport(
        OperationAnalysisContext ctx,
        RefinementService refinementService,
        string fullTypeName,
        object constantValue
    )
    {
        try
        {
            var failureMessage = refinementService.TestAgainst(fullTypeName, constantValue);

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
}
