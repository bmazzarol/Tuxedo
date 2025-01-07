#pragma warning disable RS2008

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator.Analysers;

/// <summary>
/// An analyser that ensures that the `FailureMessage` is set on the Refinement attribute, or the method returns a string
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MissingFailureMessageAnalyzer : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor Rule =
        new(
            RuleIdentifiers.MissingFailureMessage,
            "FailureMessage must be set on the Refinement attribute, or the method must return a string?",
            "FailureMessage must be set on the Refinement attribute, or the method must return a string?",
            RuleCategories.Usage,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "The FailureMessage property must be set on the Refinement attribute, or the method it on must return a string? which when non-null is used as the failure message.",
            helpLinkUri: $"https://bmazzarol.github.io/Tuxedo/rules/{RuleIdentifiers.MissingFailureMessage}.html"
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
            if (!compilationContext.Compilation.HasRefinementAttribute())
            {
                return;
            }

            compilationContext.RegisterSyntaxNodeAction(Analyze, SyntaxKind.Attribute);
        });
    }

    private static void Analyze(SyntaxNodeAnalysisContext ctx)
    {
        var type = ctx
            .SemanticModel.GetTypeInfo(ctx.Node, cancellationToken: ctx.CancellationToken)
            .Type;

        if (type?.ToDisplayString() is not "Tuxedo.RefinementAttribute")
        {
            return;
        }

        // Check if the Refinement attribute has a string passed to the constructor
        // or if the method It's applied to returns a string?
        // or if the FailureMessage property is set
        var hasStringConstructor = ctx
            .Node.DescendantNodes()
            .OfType<AttributeArgumentSyntax>()
            .Any(arg => arg.Expression is LiteralExpressionSyntax);

        var hasStringReturn = ctx
            .Node.Ancestors()
            .OfType<MethodDeclarationSyntax>()
            .Select(method =>
                ctx.SemanticModel.GetDeclaredSymbol(
                    method,
                    cancellationToken: ctx.CancellationToken
                )
            )
            .Any(method =>
                method?.ReturnType
                    is {
                        SpecialType: SpecialType.System_String,
                        NullableAnnotation: NullableAnnotation.Annotated
                    }
            );

        var hasFailureMessage = ctx
            .Node.DescendantNodes()
            .OfType<AttributeArgumentSyntax>()
            .Any(arg =>
                string.Equals(
                    arg.NameEquals?.Name.Identifier.Text,
                    "FailureMessage",
                    StringComparison.Ordinal
                )
            );

        if (hasStringConstructor || hasStringReturn || hasFailureMessage)
        {
            return;
        }

        var diagnostic = Diagnostic.Create(descriptor: Rule, location: ctx.Node.GetLocation());

        ctx.ReportDiagnostic(diagnostic);
    }
}
