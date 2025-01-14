using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator;

public sealed partial class RefinementSourceGenerator
{
    private sealed record RefinedTypeDetails(
        string? Namespace,
        SyntaxList<UsingDirectiveSyntax> Usings,
        PredicateDetails PredicateDetails,
        RefinementAttributeParts AttributeDetails,
        GenericPartDetails? GenericDetails,
        string? RawType,
        ITypeSymbol? RawTypeSymbol,
        string? RefinedType,
        string? AlternativeType,
        ITypeSymbol? AlternativeTypeSymbol
    )
    {
        public string? RefinedTypeXmlSafeName =>
            (RefinedType + GenericDetails?.Parameters).EscapeXml();

        public bool IsTuple =>
            RawType?.StartsWith("(", StringComparison.Ordinal) == true
            && RawType.EndsWith(")", StringComparison.Ordinal);
    }

    private sealed record PredicateDetails(
        string? Name,
        MethodDeclarationSyntax MethodDeclaration,
        IMethodSymbol MethodSymbol,
        bool ReturnsFailureMessage
    );

    private sealed record GenericPartDetails(
        ImmutableArray<ITypeSymbol> ParameterSymbols,
        SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintSyntaxes
    )
    {
        public string Parameters { get; } =
            $"<{ParameterSymbols.Select(t => t.ToDisplayString()).JoinBy(", ")}>";

        public string? Constraints { get; } =
            ConstraintSyntaxes.Select(x => x.ToString()).JoinBy("\n");
    }
}
