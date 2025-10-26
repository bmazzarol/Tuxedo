using System.Numerics;
using Tuxedo.Tests.Extensions;

namespace Tuxedo.Tests;

public readonly partial struct OddInt
{
    [Refinement("The number must be an odd number, but was '{value}'")]
    private static bool Odd(int value) => int.IsOddInteger(value);
}

public readonly partial struct OddT
{
    [Refinement("The number must be an odd number, but was '{value}'")]
    internal static bool Odd<T>(T value)
        where T : INumberBase<T> => T.IsOddInteger(value);
}

public class OddNumberExample
{
    [Fact(DisplayName = "A number can be refined as odd")]
    public void Case1()
    {
        var odd = OddInt.Parse(3);
        Assert.Equal(3, odd.Value);

        Assert.True(OddInt.TryParse(3, out var refined, out var message));
        Assert.Equal(3, refined.Value);
        Assert.Equal(odd, refined);
        Assert.Null(message);
    }

    [Fact(DisplayName = "A even number cannot be refined as odd")]
    public void Case2()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => (OddInt)2);

        Assert.StartsWith("The number must be an odd number, but was '2'", ex.Message);
        Assert.False(OddInt.TryParse(2, out var refined, out var message));
        Assert.Equal("The number must be an odd number, but was '2'", message);
#pragma warning disable TUX001
        Assert.Equal(default(OddInt), refined);
#pragma warning restore TUX001
    }

    [Fact(DisplayName = "A float can be refined as odd")]
    public void Case3()
    {
        var odd = OddT<float>.Parse(3f);
        Assert.Equal(3f, odd.Value);

        Assert.True(OddT<float>.TryParse(3f, out var oddInt, out var failureMessage));
        Assert.Equal(3f, oddInt.Value);
        Assert.Null(failureMessage);
        Assert.Equal(odd, oddInt);
        Assert.True(odd.Equals(oddInt));
    }

    [Fact(DisplayName = "OddT refinement snapshot is correct with generics and constraints")]
    public Task Case4()
    {
        return """
            [Refinement("The number must be an odd number, but was '{value}'", Name = "Odd")]
            internal static bool Odd<T>(T value)
                where T : System.Numerics.INumberBase<T> => T.IsOddInteger(value);
            """.VerifyRefinement();
    }

    [Fact(
        DisplayName = "OddT refinement snapshot is correct with generics and constraints and usings"
    )]
    public Task Case5()
    {
        var driver = """
            using Tuxedo;
            using System.Numerics;

            internal static class Test
            {
               [Refinement("The number must be an odd number, but was '{value}'", Name = "Odd")]
               internal static bool Odd<T>(T value) where T : INumberBase<T> => T.IsOddInteger(value);
            }
            """.BuildDriver();
        return Verify(driver).IgnoreStandardSupportCode();
    }

    [Fact(DisplayName = "OddInt refined type generates the correct ToString methods")]
    public Task Case6()
    {
        return """
            [Refinement("The number must be an odd number, but was '{value}'")]
            internal static bool Odd(int value) 
            """.VerifyRefinement();
    }

    [Fact(
        DisplayName = "OddT refinement snapshot is correct with generics and constraints on the class"
    )]
    public Task Case7()
    {
        var driver = """
            using Tuxedo;
            using System.Numerics;

            public readonly partial struct Odd<T>
               where T : INumberBase<T>
            {
               [Refinement("The number must be an odd number, but was '{value}'", Name = "Odd")]
               internal static bool Odd(T value) => T.IsOddInteger(value);
            }
            """.BuildDriver();
        return Verify(driver).IgnoreStandardSupportCode();
    }

    [Fact(
        DisplayName = "OddT refinement snapshot is correct with generics and constraints on the class and no static modifier"
    )]
    public Task Case8()
    {
        var driver = """
            using Tuxedo;
            using System.Numerics;

            public readonly partial struct Odd<T>
               where T : INumberBase<T>
            {
               [Refinement("The number must be an odd number, but was '{value}'", Name = "Odd")]
               private bool IsValid(T value) => T.IsOddInteger(value);
            }
            """.BuildDriver();
        return Verify(driver).IgnoreStandardSupportCode();
    }
}
