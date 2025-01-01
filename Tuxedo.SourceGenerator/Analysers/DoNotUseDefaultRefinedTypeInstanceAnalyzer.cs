#pragma warning disable RS2008

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator.Analysers;

/// <summary>
/// An analyser that prevents default assignment to refined types
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class DoNotUseDefaultRefinedTypeInstanceAnalyzer : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor Rule =
        new(
            RuleIdentifiers.DoNotUseDefault,
            "Using default for refined types is prohibited",
            "Type '{0}' cannot be constructed with default as it is prohibited",
            RuleCategories.Usage,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "A refined type created with a default expression is always invalid, use the Parse or TryParse method instead."
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
            var typeSymbol = compilationContext.Compilation.GetTypeByMetadataName(
                $"{compilationContext.Compilation.Assembly.Name}.RefinedTypeAttribute"
            );
            if (typeSymbol == null)
            {
                return;
            }

            compilationContext.RegisterSyntaxNodeAction(
                Analyze,
                SyntaxKind.DefaultLiteralExpression
            );
            compilationContext.RegisterSyntaxNodeAction(Analyze, SyntaxKind.DefaultExpression);
        });
    }

    private static void Analyze(SyntaxNodeAnalysisContext ctx)
    {
        var typeInfo = ctx.SemanticModel.GetTypeInfo(ctx.Node).Type;
        if (typeInfo is not INamedTypeSymbol symbol || !symbol.IsRefinedType())
        {
            return;
        }

        var diagnostic = Diagnostic.Create(
            descriptor: Rule,
            location: ctx.Node.GetLocation(),
            messageArgs: symbol.Name
        );

        ctx.ReportDiagnostic(diagnostic);
    }
}
