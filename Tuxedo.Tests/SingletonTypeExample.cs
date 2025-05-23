﻿namespace Tuxedo.Tests;

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
    public static readonly FortyTwoInt Instance = Parse(42);
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
        Assert.Equal(42, refined.Value);
        Assert.Equal(42, FortyTwoInt.Instance.Value);
    }

    [Fact(
        DisplayName = "Any other int value that is not 42 cannot be refined to the singleton type of 42"
    )]
    public void Case2()
    {
        const int value = 43;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => (FortyTwoInt)value);
        Assert.StartsWith("The value must be '42', instead found '43'", ex.Message);
        Assert.False(FortyTwoInt.TryParse(value, out _, out _));
    }

    #endregion
}
