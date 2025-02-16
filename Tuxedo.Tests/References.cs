using Microsoft.CodeAnalysis.Testing;

namespace Tuxedo.Tests;

internal static class References
{
    private static readonly string RefinementTypeAttributeAssemblyLocation =
        typeof(RefinedTypeAttribute).Assembly.Location.Replace("dll", string.Empty);

    public static readonly Lazy<ReferenceAssemblies> Net8AndOurs = new(
        () => ReferenceAssemblies.Net.Net80.AddAssemblies([RefinementTypeAttributeAssemblyLocation])
    );
}
