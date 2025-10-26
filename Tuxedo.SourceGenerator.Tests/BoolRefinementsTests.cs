using Tuxedo.Tests.Extensions;

namespace Tuxedo.Tests;

internal static class BoolRefinements
{
    [Refinement("The boolean value must be 'True', instead found '{value}'")]
    internal static bool True(bool value) => value;

    [Refinement("The boolean value must be 'False', instead found '{value}'")]
    internal static bool False(bool value) => !value;
}

public class BoolRefinementsTests
{
    [Fact(DisplayName = "A boolean value can be refined to True")]
    public void Case1()
    {
        const bool value = true;
        var refined = (TrueBool)value;
        Assert.True(refined.Value);
    }

    [Fact(DisplayName = "A boolean value can be refined to False")]
    public void Case2()
    {
        const bool value = false;
        var refined = (FalseBool)value;
        Assert.False(refined.Value);
    }

    [Fact(DisplayName = "A False refinement should fail when the value is True")]
    public void Case3()
    {
        const bool value = true;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => (FalseBool)value);
        Assert.StartsWith("The boolean value must be 'False', instead found 'True'", ex.Message);
        Assert.Equal(value, ex.ActualValue);
        Assert.Equal("value", ex.ParamName);
        Assert.False(FalseBool.TryParse(value, out _, out _));
    }

    [Fact(DisplayName = "FalseBool refinement snapshot is correct")]
    public Task Case4()
    {
        return """
            [Refinement("The boolean value must be 'False', instead found '{value}'")]
            internal static bool False(bool value) => !value;
            """.VerifyRefinement();
    }

    [Fact(DisplayName = "TrueBool refinement snapshot is correct")]
    public Task Case5()
    {
        return """
            [Refinement("The boolean value must be 'True', instead found '{value}'")]
            internal static bool True(bool value) => value;
            """.VerifyRefinement();
    }
}
