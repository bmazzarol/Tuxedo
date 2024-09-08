using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tuxedo.SourceGenerators;

public sealed partial class RefinementSourceGenerator
{
    private readonly ref struct RefinementAttributeParts
    {
        public string FailureMessage { get; }
        private bool IsInternal { get; }
        public string AccessModifier => IsInternal ? "internal" : "public";

        public RefinementAttributeParts(MethodDeclarationSyntax methodDeclaration)
        {
            var arguments = methodDeclaration
                .AttributeLists.SelectMany(list => list.Attributes)
                .Single(attribute => attribute.Name.ToString() == "Refinement")
                .ArgumentList!.Arguments;

            FailureMessage = arguments[0].Expression.ToString();
            IsInternal = arguments.Count > 1 && arguments[1].Expression.ToString() == "false";
        }
    }
}
