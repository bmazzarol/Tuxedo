using Microsoft.CodeAnalysis;

namespace Tuxedo.SourceGenerator.Extensions;

internal static class CompilationExtensions
{
    public static bool HasRefinedTypeAttribute(this Compilation compilation)
    {
        var typeSymbol = compilation.GetTypeByMetadataName("Tuxedo.RefinedTypeAttribute");
        return typeSymbol != null;
    }
}
