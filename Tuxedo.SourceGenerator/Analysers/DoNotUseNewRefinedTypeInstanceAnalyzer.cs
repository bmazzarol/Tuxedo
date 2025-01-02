#pragma warning disable RS2008

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator.Analysers;

/// <summary>
/// An analyser that prevents new construction of refined types
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class DoNotUseNewRefinedTypeInstanceAnalyzer : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor Rule =
        new(
            RuleIdentifiers.DoNotUseNew,
            "Using new to construct refined types is prohibited",
            "Type '{0}' cannot be constructed with the new keyword as it is prohibited",
            RuleCategories.Usage,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "A refined type created with the new keyword is always invalid, use the Parse or TryParse method instead.",
            helpLinkUri: $"https://bmazzarol.github.io/Tuxedo/rules/{RuleIdentifiers.DoNotUseNew}.html"
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

            compilationContext.RegisterOperationAction(Analyze, OperationKind.ObjectCreation);
        });
    }

    private static void Analyze(OperationAnalysisContext ctx)
    {
        if (
            ctx.Operation is not IObjectCreationOperation { Type: INamedTypeSymbol symbol } o
            || !symbol.IsRefinedType()
        )
        {
            return;
        }

        var diagnostic = Diagnostic.Create(
            descriptor: Rule,
            location: o.Syntax.GetLocation(),
            messageArgs: symbol.Name
        );

        ctx.ReportDiagnostic(diagnostic);
    }
}
