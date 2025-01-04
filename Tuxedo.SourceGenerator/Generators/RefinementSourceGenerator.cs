#pragma warning disable MA0051

using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator;

/// <summary>
/// Primary source generator that builds refined types from predicate methods
/// </summary>
[Generator]
public sealed partial class RefinementSourceGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            ctx.AddSource("RefinementAttribute.g", RefinementAttributeSource);
            ctx.AddSource("RefinedTypeAttribute.g", RefinedTypeAttributeSource);
        });

        var refinedTypeDetailsProvider = context.SyntaxProvider.ForAttributeWithMetadataName(
            "Tuxedo.RefinementAttribute",
            IsRefinementMethod,
            BuildRefinedTypeDetails
        );

        context.RegisterSourceOutput(refinedTypeDetailsProvider, GenerateRefinedTypes);
        context.RegisterSourceOutput(
            refinedTypeDetailsProvider.Collect(),
            GenerateRefinementService
        );
    }

    private static bool IsRefinementMethod(SyntaxNode s, CancellationToken cancellationToken)
    {
        return s is MethodDeclarationSyntax mds
            && mds.Modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword))
            // and returns a bool
            && string.Equals(mds.ReturnType.ToString(), "bool", StringComparison.Ordinal)
            // and has at least one parameter and no more than two
            && mds.ParameterList.Parameters.Count is >= 1 and <= 2
            // and if there are two parameters, the second is an out parameter
            && (
                mds.ParameterList.Parameters.Count == 1
                || mds.ParameterList.Parameters[1]
                    .Modifiers.Any(m => m.IsKind(SyntaxKind.OutKeyword))
            );
    }

    private static RefinedTypeDetails BuildRefinedTypeDetails(
        GeneratorAttributeSyntaxContext ctx,
        CancellationToken token
    )
    {
        var methodDeclarationSyntax = (MethodDeclarationSyntax)ctx.TargetNode;

        // get the namespace
        var methodSymbol = ctx.SemanticModel.GetDeclaredSymbol(
            methodDeclarationSyntax,
            cancellationToken: token
        )!;
        var ns = methodSymbol.ContainingNamespace.ToDisplayString();

        // get the predicate details
        var containingType = methodSymbol.ContainingType;
        var @class = containingType.ToDisplayString();
        var name = methodDeclarationSyntax.Identifier.Text;
        var predicate = $"{@class}.{name}";

        // get the attribute details
        var attributeParts = new RefinementAttributeParts(methodDeclarationSyntax);
        var failureMessage = attributeParts.FailureMessage;
        var accessModifier = attributeParts.AccessModifier;

        // extract the generic parts
        ExtractGenericPartDetails(
            methodSymbol,
            methodDeclarationSyntax,
            out var generics,
            out var genericTypeConstraints
        );

        // get the first parameter type
        var firstParam = methodDeclarationSyntax.ParameterList.Parameters[0].Type!;
        var firstParameterTypeInfo = ctx
            .SemanticModel.GetTypeInfo(firstParam, cancellationToken: token)
            .Type;
        var rawType = firstParameterTypeInfo!.ToDisplayString();

        // build the name of the refined type
        var refinedType = BuildSafeStructName(
            attributeParts.Name ?? name,
            attributeParts.Name is not null
                ? null
                : rawType.UppercaseFirst()?.RemoveNamespace().RemoveGenerics()
        );

        // try and see if there is a second out parameter
        string? altType = null;
        if (methodDeclarationSyntax.ParameterList.Parameters.Count > 1)
        {
            var secondParam = methodDeclarationSyntax.ParameterList.Parameters[1].Type!;
            var secondParameterTypeInfo = ctx
                .SemanticModel.GetTypeInfo(secondParam, cancellationToken: token)
                .Type;
            altType = secondParameterTypeInfo!.ToDisplayString();
        }

        return new RefinedTypeDetails(
            Namespace: ns,
            Predicate: predicate,
            FailureMessage: failureMessage,
            AccessModifier: accessModifier,
            Generics: generics,
            GenericConstraints: genericTypeConstraints,
            RawType: rawType,
            RefinedType: refinedType,
            AlternativeType: altType
        );
    }

    private static void ExtractGenericPartDetails(
        IMethodSymbol methodSymbol,
        MethodDeclarationSyntax methodDeclaration,
        out string? generics,
        out string? constraints
    )
    {
        generics = null;
        constraints = null;

        var genericTypeArguments = methodSymbol.TypeArguments;
        if (genericTypeArguments.Length == 0)
        {
            return;
        }

        generics = $"<{string.Join(", ", genericTypeArguments.Select(t => t.ToDisplayString()))}>";
        var parts = methodDeclaration.ConstraintClauses.Select(x => x.ToString()).ToArray();
        constraints = parts.Length > 0 ? string.Join("\n", parts) : null;
    }

    private static string BuildSafeStructName(string name, string? parameterType)
    {
        return parameterType == null ? name : $"{name}{parameterType}";
    }

    private static void GenerateRefinedTypes(
        SourceProductionContext context,
        RefinedTypeDetails refinedTypeDetails
    )
    {
        var source = refinedTypeDetails.AlternativeType is not null
            ? RenderMultiRefinedType(refinedTypeDetails)
            : RenderSingleRefinedType(refinedTypeDetails);

        context.AddSource(
            $"{refinedTypeDetails.RefinedType}.g.cs",
            SourceText.From(source, Encoding.UTF8)
        );
    }

    private static void GenerateRefinementService(
        SourceProductionContext context,
        ImmutableArray<RefinedTypeDetails> refinedTypeDetails
    )
    {
        context.AddSource(
            "RefinementService.g.cs",
            SourceText.From(RenderRefinementService(refinedTypeDetails), Encoding.UTF8)
        );
    }
}
