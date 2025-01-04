using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator;

public sealed partial class RefinementSourceGenerator
{
    private readonly record struct RefinedTypeDetails(
        string? Namespace,
        string? Predicate,
        string? FailureMessage,
        string? AccessModifier,
        string? Generics,
        string? GenericConstraints,
        string? RawType,
        string? RefinedType,
        string? AlternativeType
    )
    {
        public string? RefinedTypeXmlSafeName => (RefinedType + Generics).EscapeXml();

        public bool IsTuple =>
            RawType?.StartsWith("(", StringComparison.Ordinal) == true
            && RawType.EndsWith(")", StringComparison.Ordinal);
    }
}
