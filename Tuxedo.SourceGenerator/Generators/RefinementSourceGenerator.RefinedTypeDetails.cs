using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator;

public sealed partial class RefinementSourceGenerator
{
    private sealed record RefinedTypeDetails(
        string? Namespace,
        SyntaxList<UsingDirectiveSyntax> Usings,
        string? Predicate,
        bool PredicateReturnsFailureMessage,
        RefinementAttributeParts AttributeDetails,
        string? Generics,
        string? GenericConstraints,
        string? RawType,
        ITypeSymbol? RawTypeSymbol,
        string? RefinedType,
        string? AlternativeType,
        ITypeSymbol? AlternativeTypeSymbol
    )
    {
        public string? RefinedTypeXmlSafeName => (RefinedType + Generics).EscapeXml();

        public bool IsTuple =>
            RawType?.StartsWith("(", StringComparison.Ordinal) == true
            && RawType.EndsWith(")", StringComparison.Ordinal);
    }
}
