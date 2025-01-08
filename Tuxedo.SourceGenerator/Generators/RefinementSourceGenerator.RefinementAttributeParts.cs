using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator;

public sealed partial class RefinementSourceGenerator
{
    private sealed record RefinementAttributeParts
    {
        public string? FailureMessage { get; }
        private bool IsInternal { get; }
        public string AccessModifier => IsInternal ? "internal" : "public";
        public string? Name { get; }
        public bool HasImplicitConversionFromRaw { get; }

        public RefinementAttributeParts(MethodDeclarationSyntax methodDeclaration)
        {
            var arguments = methodDeclaration
                .AttributeLists.SelectMany(list => list.Attributes)
                .Single(attribute =>
                    string.Equals(attribute.Name.ToString(), "Refinement", StringComparison.Ordinal)
                    || string.Equals(
                        attribute.Name.ToString(),
                        "Tuxedo.Refinement",
                        StringComparison.Ordinal
                    )
                )
                .ArgumentList!.Arguments;

            var nameToArgs = arguments
                .Where(x => x.NameEquals is not null)
                .ToDictionary(
                    x => x.NameEquals?.Name.ToString(),
                    syntax => syntax.Expression.ToString(),
                    StringComparer.OrdinalIgnoreCase
                );
            FailureMessage = nameToArgs.TryGetValue(nameof(FailureMessage), out var failureMessage)
                ? failureMessage
                : arguments
                    .Where(x => x.NameEquals is null)
                    .Select(x => x.Expression.ToString())
                    .FirstOrDefault();
            Name = nameToArgs.TryGetValue(nameof(Name), out var nameToName)
                ? nameToName.StripExpressionParts()
                : null;
            IsInternal = IsEnabled(nameof(IsInternal), nameToArgs);
            HasImplicitConversionFromRaw = IsEnabled(
                nameof(HasImplicitConversionFromRaw),
                nameToArgs
            );
        }

        private static bool IsEnabled(string value, Dictionary<string?, string> nameToArgs) =>
            nameToArgs.TryGetValue(value, out var v)
            && string.Equals(v, "true", StringComparison.Ordinal);
    }
}
