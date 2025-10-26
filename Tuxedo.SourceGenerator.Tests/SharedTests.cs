using Tuxedo.Tests.Extensions;

namespace Tuxedo.Tests;

public sealed class SharedTests
{
    [Fact(DisplayName = "All shared code renders correctly")]
    public Task Case1()
    {
        var driver = """
            using Tuxedo;

            internal static class Test
            {
               [Refinement("test1", Name = "Test1")]
               internal static bool Pred1(bool value) => !value;
               
               [Refinement("test2", Name = "Test2")]
               internal static bool Pred2((int a, int b) value) => true;
               
               [Refinement("test2", Name = "Test3")]
               internal static bool Pred3<T>(List<T> value) => true;
               
               [Refinement("test2", Name = "Test4")]
               internal static bool Pred4<T>(List<T> value) where T: struct => true;
            }
            """.BuildDriver();
        return Verify(driver).IgnoreGeneratedResult(result => result.HintName.StartsWith("Test"));
    }
}
