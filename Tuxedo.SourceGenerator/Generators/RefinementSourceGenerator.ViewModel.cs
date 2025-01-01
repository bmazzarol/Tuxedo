using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator;

public sealed partial class RefinementSourceGenerator
{
    private sealed class ViewModel
    {
        public string? Namespace { get; set; }
        public string? RawType { get; set; }

        public string? SafeRawTypeName() =>
            RawType?.UppercaseFirst()?.RemoveNamespace().RemoveGenerics();

        public string? AlternativeType { get; set; }
        public string? Predicate { get; set; }
        public string? AccessModifier { get; set; }
        public string? RefinedType { get; set; }
        public string? RefinedTypeXmlSafeName => (RefinedType + Generics).EscapeXml();
        public string? FailureMessage { get; set; }
        public string? Generics { get; set; }
        public string? GenericConstraints { get; set; }
    }
}
