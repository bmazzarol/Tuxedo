using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator;

public sealed partial class RefinementSourceGenerator
{
    private readonly record struct RefinedTypeDetails(
        string? Namespace,
        SyntaxList<UsingDirectiveSyntax> Usings,
        string? Predicate,
        bool PredicateReturnsFailureMessage,
        string? FailureMessage,
        string? AccessModifier,
        string? Generics,
        string? GenericConstraints,
        string? RawType,
        string? RefinedType,
        string? AlternativeType,
        bool HasImplicitConversionFromRaw
    )
    {
        public string? RefinedTypeXmlSafeName => (RefinedType + Generics).EscapeXml();

        public bool IsTuple =>
            RawType?.StartsWith("(", StringComparison.Ordinal) == true
            && RawType.EndsWith(")", StringComparison.Ordinal);
    }
}
