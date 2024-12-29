using Tuxedo.Tests.Extensions;

namespace Tuxedo.Tests;

public sealed class SharedTests
{
    [Fact(DisplayName = "A")]
    public Task Case1()
    {
        var driver = GeneratorDriverExtensions.BuildDriver(source: null);
        return Verify(driver);
    }
}
