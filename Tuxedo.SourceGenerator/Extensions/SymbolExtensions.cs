using Microsoft.CodeAnalysis;

namespace Tuxedo.SourceGenerator;

internal static class SymbolExtensions
{
    public static bool IsRefinedType(this INamedTypeSymbol symbol)
    {
        var attrs = symbol.GetAttributes();
        return attrs.Any(a =>
            a
                .AttributeClass?.ToDisplayString()
                .Equals("Tuxedo.RefinedTypeAttribute", StringComparison.Ordinal)
                is true
        );
    }

    public static bool HasInterface(this ITypeSymbol? symbol, string interfaceName)
    {
        return symbol?.AllInterfaces.Any(i =>
                i.ToDisplayString().Equals(interfaceName, StringComparison.Ordinal)
            ) ?? false;
    }
}
