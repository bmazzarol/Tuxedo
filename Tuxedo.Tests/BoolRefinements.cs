using FluentAssertions;
using Xunit;

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
        Refined<bool, True> refined = value;
        refined.Value.Should().BeTrue();
    }

    [Fact(DisplayName = "A boolean value can be refined to False")]
    public void Case2()
    {
        const bool value = false;
        Refined<bool, False> refined = value;
        refined.Value.Should().BeFalse();
    }

    [Fact(DisplayName = "A False refinement should fail when the value is True")]
    public void Case3()
    {
        const bool value = true;
        Assert
            .Throws<RefinementFailureException>(() => (Refined<bool, False>)value)
            .Message.Should()
            .Be("The boolean value must be 'False', instead found 'True'");
        Refined.TryRefine(value, out Refined<bool, False> _).Should().BeFalse();
    }
}
