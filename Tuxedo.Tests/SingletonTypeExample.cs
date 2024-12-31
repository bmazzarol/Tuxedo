using FluentAssertions;

namespace Tuxedo.Tests;

#region SingletonTypeExample

/// <summary>
/// This is a singleton type, it's an int that can only hold the value 42
/// </summary>
public readonly partial struct FortyTwoInt
{
    [Refinement("The value must be '42', instead found '{value}'")]
    private static bool FortyTwo(int value) => value == 42;

    /// <summary>
    /// The singleton instance of 42
    /// </summary>
    public static readonly FortyTwoInt Instance = new(42);
}

#endregion

public sealed class GenericRefinementTests
{
    #region SingletonTypeUsage

    [Fact(DisplayName = "The value 42 can be refined to a singleton type")]
    public void Case1()
    {
        const int value = 42;
        var refined = (FortyTwoInt)value;
        refined.Value.Should().Be(42);
        FortyTwoInt.Instance.Value.Should().Be(42);
    }

    [Fact(
        DisplayName = "Any other int value that is not 42 cannot be refined to the singleton type of 42"
    )]
    public void Case2()
    {
        const int value = 43;
        Assert
            .Throws<ArgumentOutOfRangeException>(() => (FortyTwoInt)value)
            .Message.Should()
            .StartWith("The value must be '42', instead found '43'");
        FortyTwoInt.TryParse(value, out _, out _).Should().BeFalse();
    }

    #endregion
}
