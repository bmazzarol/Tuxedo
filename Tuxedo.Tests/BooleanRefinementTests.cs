namespace Tuxedo.Tests;

public static class BooleanRefinementTests
{
    [Fact(DisplayName = "A true boolean can be refined")]
    public static void Case1()
    {
        Refined<bool, True> refined = true;
        ((bool)refined).Should().BeTrue();
        Refined.TryRefine<bool, True>(true, out _).Should().BeTrue();
        if (Refined.TryRefine<bool, True>(true, out var test))
        {
            ((bool)test).Should().BeTrue();
        }
        else
        {
            throw new InvalidOperationException("Refinement failed");
        }
    }

    [Fact(DisplayName = "A false boolean cannot be refined")]
    public static void Case2()
    {
        Refined.TryRefine<bool, True>(false, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A false boolean cannot be refined and throws")]
    public static void Case3()
    {
        var act = () => (Refined<bool, True>)false;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("The boolean value must be 'True', instead found 'False'")
            .And.Value.Should()
            .BeOfType<bool>()
            .Which.Should()
            .BeFalse();
    }

    [Fact(DisplayName = "A false boolean can be refined")]
    public static void Case4()
    {
        Refined<bool, False> refined = false;
        refined.Value.Should().BeFalse();
        Refined.TryRefine<bool, False>(false, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A true boolean cannot be refined")]
    public static void Case5()
    {
        Refined.TryRefine<bool, False>(true, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A true boolean cannot be refined and throws")]
    public static void Case6()
    {
        var act = () => (Refined<bool, False>)true;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("The boolean value must be 'False', instead found 'True'")
            .And.Value.Should()
            .BeOfType<bool>()
            .Which.Should()
            .BeTrue();
    }

    [Fact(DisplayName = "A refinement can be inverted")]
    public static void Case7()
    {
        Refined<bool, Not<bool, True>> refined = false;
        (refined is { Value: false }).Should().BeTrue();
        Refined.TryRefine<bool, Not<bool, True>>(false, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "An inverted refinement fail")]
    public static void Case8()
    {
        Refined.TryRefine<bool, Not<bool, True>>(true, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "An inverted refinement fail and throws")]
    public static void Case9()
    {
        var act = () => (Refined<bool, Not<bool, True>>)true;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Refinement 'True' passed when it should have failed")
            .And.Value.Should()
            .BeOfType<bool>()
            .Which.Should()
            .BeTrue();
    }
}
