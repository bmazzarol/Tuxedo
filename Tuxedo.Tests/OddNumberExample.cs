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
        where T : System.Numerics.INumberBase<T> => T.IsOddInteger(value);
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
            .Throws<InvalidOperationException>(() => (OddInt)2)
            .Message.Should()
            .Be("The number must be an odd number, but was '2'");
        OddInt.TryParse(2, out var refined, out var message).Should().BeFalse();
        message.Should().Be("The number must be an odd number, but was '2'");
        refined.Should().Be(default(OddInt));
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
}
