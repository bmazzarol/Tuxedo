using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tuxedo.SourceGenerator;

internal sealed record GenericPartDetails(
    ImmutableArray<ITypeSymbol> ParameterSymbols,
    SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintSyntaxes
)
{
    public string Parameters { get; } =
        $"<{ParameterSymbols.Select(t => t.ToDisplayString()).JoinBy(", ")}>";

    public string? Constraints { get; } = ConstraintSyntaxes.Select(x => x.ToString()).JoinBy("\n");
}
