using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tuxedo.SourceGenerator;

public sealed partial class RefinementSourceGenerator
{
    private readonly ref struct RefinementAttributeParts
    {
        private static readonly Dictionary<int, string> ParameterNameLookup =
            new()
            {
                [0] = nameof(FailureMessage),
                [1] = nameof(IsInternal),
                [2] = nameof(DropTypeFromName),
            };

        public string FailureMessage { get; }
        private bool IsInternal { get; }
        public string AccessModifier => IsInternal ? "internal" : "public";
        public bool DropTypeFromName { get; }

        public RefinementAttributeParts(MethodDeclarationSyntax methodDeclaration)
        {
            var arguments = methodDeclaration
                .AttributeLists.SelectMany(list => list.Attributes)
                .Single(attribute =>
                    string.Equals(attribute.Name.ToString(), "Refinement", StringComparison.Ordinal)
                )
                .ArgumentList!.Arguments;

            var nameToArgs = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var kvp in ParameterNameLookup)
            {
                var argument = arguments.Count > kvp.Key ? arguments[kvp.Key] : null;
                nameToArgs[kvp.Value] = argument?.Expression.ToString() ?? string.Empty;
            }

            FailureMessage = nameToArgs[nameof(FailureMessage)];
            IsInternal = string.Equals(
                nameToArgs[nameof(IsInternal)],
                "true",
                StringComparison.OrdinalIgnoreCase
            );
            DropTypeFromName = string.Equals(
                nameToArgs[nameof(DropTypeFromName)],
                "true",
                StringComparison.OrdinalIgnoreCase
            );
        }
    }
}
