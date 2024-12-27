using FluentAssertions;
using Xunit;

namespace Tuxedo.Tests;

public static class GenericRefinements
{
    [Refinement("The value must be '42', instead found '{value}'")]
    internal static bool FortyTwo(int value) => Equals(value, 42);
}

public sealed class GenericRefinementTests
{
    [Fact(DisplayName = "A value can be refined to a constant value")]
    public void Case1()
    {
        const int value = 42;
        var refined = (FortyTwoInt)value;
        refined.Value.Should().Be(42);
    }

    [Fact(
        DisplayName = "A value that is not equal to the constant value should fail the refinement"
    )]
    public void Case2()
    {
        const int value = 43;
        Assert
            .Throws<InvalidOperationException>(() => (FortyTwoInt)value)
            .Message.Should()
            .Be("The value must be '42', instead found '43'");
        FortyTwoInt.TryParse(value, out _, out _).Should().BeFalse();
    }
}
