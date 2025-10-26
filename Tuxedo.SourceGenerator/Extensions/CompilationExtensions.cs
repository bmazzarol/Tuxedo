using Microsoft.CodeAnalysis;

namespace Tuxedo.SourceGenerator;

internal static class CompilationExtensions
{
    public static bool HasRefinedTypeAttribute(this Compilation compilation)
    {
        var typeSymbol = compilation.GetTypeByMetadataName("Tuxedo.RefinedTypeAttribute");
        return typeSymbol != null;
    }

    public static bool HasRefinementAttribute(this Compilation compilation)
    {
        var typeSymbol = compilation.GetTypeByMetadataName("Tuxedo.RefinementAttribute");
        return typeSymbol != null;
    }
}
