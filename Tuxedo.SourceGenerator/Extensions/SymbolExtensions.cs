using Microsoft.CodeAnalysis;

namespace Tuxedo.SourceGenerator.Extensions;

internal static class SymbolExtensions
{
    public static bool IsRefinedType(this INamedTypeSymbol? symbol)
    {
        if (symbol == null)
        {
            return false;
        }

        var attrs = symbol.GetAttributes();
        return attrs.Any(a =>
            a
                .AttributeClass?.ToDisplayString()
                .EndsWith(".RefinedTypeAttribute", StringComparison.Ordinal)
                is true
        );
    }
}
