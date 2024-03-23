namespace Tuxedo.Tests;

public static class IntRefinementTests
{
    [Fact(DisplayName = "A positive integer can be refined")]
    public static void Case1()
    {
        var refined = (Refined<int, Positive>)1;
        ((int)refined).Should().Be(1);
        Refined.TryRefine<int, Positive>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A negative integer cannot be refined")]
    public static void Case2()
    {
        Refined.TryRefine<int, Positive>(-1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A negative integer cannot be refined and throws")]
    public static void Case3()
    {
        var act = () => Refined.Refine<int, Positive>(-1);
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be positive")
            .And.Value.Should()
            .Be(-1);
    }

    private static int OnlyAcceptPositiveInt(int value, Refined<int, Positive> divisor)
    {
        return value / divisor;
    }

    [Fact(DisplayName = "A positive integer can be used in a method")]
    public static void Case4()
    {
        var result = OnlyAcceptPositiveInt(10, 2);
        result.Should().Be(5);
    }

    [Fact(DisplayName = "A negative integer cannot be used in a method")]
    public static void Case5()
    {
        var act = () => OnlyAcceptPositiveInt(10, Refined.Refine<int, Positive>(-2));
        var ex = act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be positive");
        ex.And.Value.Should().Be(-2);
        // todo: ensure the stack trace is correct
    }

    [Fact(DisplayName = "An even integer can be refined")]
    public static void Case6()
    {
        var refined = (Refined<int, Even>)2;
        ((int)refined).Should().Be(2);
        Refined.TryRefine<int, Even>(2, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "An odd integer cannot be refined")]
    public static void Case7()
    {
        Refined.TryRefine<int, Even>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "An odd integer cannot be refined and throws")]
    public static void Case8()
    {
        var act = () => Refined.Refine<int, Even>(1);
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be even")
            .And.Value.Should()
            .Be(1);
    }

    [Fact(DisplayName = "Using Not odd integers can be refined with even")]
    public static void Case9()
    {
        var refined = (Refined<int, Not<Even, int>>)1;
        ((int)refined).Should().Be(1);
        Refined.TryRefine<int, Not<Even, int>>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "Using Not even integers cannot be refined with even")]
    public static void Case10()
    {
        Refined.TryRefine<int, Not<Even, int>>(2, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "Using Not even integers cannot be refined with even and throws")]
    public static void Case11()
    {
        var act = () => Refined.Refine<int, Not<Even, int>>(2);
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Not, Value must be even")
            .And.Value.Should()
            .Be(2);
    }
}
