namespace Tuxedo.Tests.Numeric;

public static class NegativeTests
{
    [Fact(DisplayName = "A negative sbyte can be refined")]
    public static void Case1()
    {
        NegativeSByte refined = -1;
        ((sbyte)refined).Should().Be(-1);
        Refined.TryRefine<sbyte, Negative>(-1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A positive sbyte cannot be refined")]
    public static void Case2()
    {
        Refined.TryRefine<sbyte, Negative>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A positive sbyte cannot be refined and throws")]
    public static void Case3()
    {
        var act = () => (NegativeSByte)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be negative, but found 1")
            .And.Value.Should()
            .BeOfType<sbyte>()
            .And.Be(1);
    }

    [Fact(DisplayName = "A negative short can be refined")]
    public static void Case4()
    {
        NegativeShort refined = -1;
        ((short)refined).Should().Be(-1);
        Refined.TryRefine<short, Negative>(-1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A positive short cannot be refined")]
    public static void Case5()
    {
        Refined.TryRefine<short, Negative>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A positive short cannot be refined and throws")]
    public static void Case6()
    {
        var act = () => (NegativeShort)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be negative, but found 1")
            .And.Value.Should()
            .BeOfType<short>()
            .And.Be(1);
    }

    #region NegativeInt

    [Fact(DisplayName = "A negative int can be refined")]
    public static void Case7()
    {
        NegativeInt refined = -1;
        ((int)refined).Should().Be(-1);
        Refined.TryRefine<int, Negative>(-1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A positive int cannot be refined")]
    public static void Case8()
    {
        Refined.TryRefine<int, Negative>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A positive int cannot be refined and throws")]
    public static void Case9()
    {
        var act = () => (NegativeInt)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be negative, but found 1")
            .And.Value.Should()
            .BeOfType<int>()
            .And.Be(1);
    }

    #endregion

    [Fact(DisplayName = "A negative long can be refined")]
    public static void Case10()
    {
        NegativeLong refined = -1;
        ((long)refined).Should().Be(-1);
        Refined.TryRefine<long, Negative>(-1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A positive long cannot be refined")]
    public static void Case11()
    {
        Refined.TryRefine<long, Negative>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A positive long cannot be refined and throws")]
    public static void Case12()
    {
        var act = () => (NegativeLong)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be negative, but found 1")
            .And.Value.Should()
            .BeOfType<long>()
            .And.Be(1);
    }

    [Fact(DisplayName = "A negative float can be refined")]
    public static void Case13()
    {
        NegativeFloat refined = -1;
        ((float)refined).Should().Be(-1);
        Refined.TryRefine<float, Negative>(-1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A positive float cannot be refined")]
    public static void Case14()
    {
        Refined.TryRefine<float, Negative>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A positive float cannot be refined and throws")]
    public static void Case15()
    {
        var act = () => (NegativeFloat)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be negative, but found 1")
            .And.Value.Should()
            .BeOfType<float>()
            .And.Be(1);
    }

    [Fact(DisplayName = "A negative double can be refined")]
    public static void Case16()
    {
        NegativeDouble refined = -1;
        ((double)refined).Should().Be(-1);
        Refined.TryRefine<double, Negative>(-1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A positive double cannot be refined")]
    public static void Case17()
    {
        Refined.TryRefine<double, Negative>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A positive double cannot be refined and throws")]
    public static void Case18()
    {
        var act = () => (NegativeDouble)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be negative, but found 1")
            .And.Value.Should()
            .BeOfType<double>()
            .And.Be(1);
    }

    [Fact(DisplayName = "A negative decimal can be refined")]
    public static void Case19()
    {
        NegativeDecimal refined = -1;
        ((decimal)refined).Should().Be(-1);
        Refined.TryRefine<decimal, Negative>(-1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A positive decimal cannot be refined")]
    public static void Case20()
    {
        Refined.TryRefine<decimal, Negative>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A positive decimal cannot be refined and throws")]
    public static void Case21()
    {
        var act = () => (NegativeDecimal)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be negative, but found 1")
            .And.Value.Should()
            .BeOfType<decimal>()
            .And.Be(1);
    }
}
