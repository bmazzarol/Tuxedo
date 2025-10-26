using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tuxedo.SourceGenerator;

internal sealed record RefinedTypeDetails(
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
    public string? RefinedTypeXmlSafeName => (RefinedType + GenericDetails?.Parameters).EscapeXml();

    public bool IsTuple =>
        RawType?.StartsWith("(", StringComparison.Ordinal) == true
        && RawType.EndsWith(")", StringComparison.Ordinal);
}
