using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator;

public sealed partial class RefinementSourceGenerator
{
    private readonly ref struct RefinementAttributeParts
    {
        public string? FailureMessage { get; }
        private bool IsInternal { get; }
        public string AccessModifier => IsInternal ? "internal" : "public";
        public string? Name { get; }

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
            IsInternal =
                nameToArgs.TryGetValue(nameof(IsInternal), out var value)
                && string.Equals(value, "true", StringComparison.Ordinal);
            Name = nameToArgs.TryGetValue(nameof(Name), out var nameToName)
                ? nameToName.StripExpressionParts()
                : null;
        }
    }
}
