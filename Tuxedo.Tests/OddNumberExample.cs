using System.Numerics;
using FluentAssertions;
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
        odd.Value.Should().Be(3);

        OddInt.TryParse(3, out var oddInt, out var failureMessage).Should().BeTrue();
        oddInt.Value.Should().Be(3);
        failureMessage.Should().BeNull();
        (odd == oddInt).Should().BeTrue();
    }

    [Fact(DisplayName = "A even number cannot be refined as odd")]
    public void Case2()
    {
        Assert
            .Throws<ArgumentOutOfRangeException>(() => (OddInt)2)
            .Message.Should()
            .StartWith("The number must be an odd number, but was '2'");
        OddInt.TryParse(2, out var refined, out var message).Should().BeFalse();
        message.Should().Be("The number must be an odd number, but was '2'");
#pragma warning disable TUX001
        refined.Should().Be(default(OddInt));
#pragma warning restore TUX001
    }

    [Fact(DisplayName = "A float can be refined as odd")]
    public void Case3()
    {
        var odd = OddT<float>.Parse(3f);
        odd.Value.Should().Be(3f);

        OddT<float>.TryParse(3f, out var oddInt, out var failureMessage).Should().BeTrue();
        oddInt.Value.Should().Be(3f);
        failureMessage.Should().BeNull();
        (odd == oddInt).Should().BeTrue();
        odd.Equals(oddInt).Should().BeTrue();
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
}
