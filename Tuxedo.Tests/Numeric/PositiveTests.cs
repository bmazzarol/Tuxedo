namespace Tuxedo.Tests.Numeric;

public static class PositiveTests
{
    [Fact(DisplayName = "A positive sbyte can be refined")]
    public static void Case1()
    {
        PositiveSByte refined = 1;
        ((sbyte)refined).Should().Be(1);
        Refined.TryRefine<sbyte, Positive>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A negative sbyte cannot be refined")]
    public static void Case2()
    {
        Refined.TryRefine<sbyte, Positive>(0, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A negative sbyte cannot be refined and throws")]
    public static void Case3()
    {
        var act = () => (PositiveSByte)0;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be positive, but found 0")
            .And.Value.Should()
            .BeOfType<sbyte>()
            .And.Be(0);
    }

    [Fact(DisplayName = "A positive short can be refined")]
    public static void Case4()
    {
        PositiveShort refined = 1;
        ((short)refined).Should().Be(1);
        Refined.TryRefine<short, Positive>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A negative short cannot be refined")]
    public static void Case5()
    {
        Refined.TryRefine<short, Positive>(0, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A negative short cannot be refined and throws")]
    public static void Case6()
    {
        var act = () => (PositiveShort)0;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be positive, but found 0")
            .And.Value.Should()
            .BeOfType<short>()
            .And.Be(0);
    }

    #region PositiveInt

    [Fact(DisplayName = "A positive int can be refined")]
    public static void Case7()
    {
        PositiveInt refined = 1;
        ((int)refined).Should().Be(1);
        Refined.TryRefine<int, Positive>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A negative int cannot be refined")]
    public static void Case8()
    {
        Refined.TryRefine<int, Positive>(0, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A negative int cannot be refined and throws")]
    public static void Case9()
    {
        var act = () => (PositiveInt)0;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be positive, but found 0")
            .And.Value.Should()
            .BeOfType<int>()
            .And.Be(0);
    }

    #endregion

    [Fact(DisplayName = "A positive long can be refined")]
    public static void Case10()
    {
        PositiveLong refined = 1;
        ((long)refined).Should().Be(1);
        Refined.TryRefine<long, Positive>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A negative long cannot be refined")]
    public static void Case11()
    {
        Refined.TryRefine<long, Positive>(0, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A negative long cannot be refined and throws")]
    public static void Case12()
    {
        var act = () => (PositiveLong)0;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be positive, but found 0")
            .And.Value.Should()
            .BeOfType<long>()
            .And.Be(0);
    }

    [Fact(DisplayName = "A positive byte can be refined")]
    public static void Case13()
    {
        PositiveByte refined = 1;
        ((byte)refined).Should().Be(1);
        Refined.TryRefine<byte, Positive>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A negative byte cannot be refined")]
    public static void Case14()
    {
        Refined.TryRefine<byte, Positive>(0, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A negative byte cannot be refined and throws")]
    public static void Case15()
    {
        var act = () => (PositiveByte)0;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be positive, but found 0")
            .And.Value.Should()
            .BeOfType<byte>()
            .And.Be(0);
    }

    [Fact(DisplayName = "A positive ushort can be refined")]
    public static void Case16()
    {
        PositiveUShort refined = 1;
        ((ushort)refined).Should().Be(1);
        Refined.TryRefine<ushort, Positive>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A negative ushort cannot be refined")]
    public static void Case17()
    {
        Refined.TryRefine<ushort, Positive>(0, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A negative ushort cannot be refined and throws")]
    public static void Case18()
    {
        var act = () => (PositiveUShort)0;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be positive, but found 0")
            .And.Value.Should()
            .BeOfType<ushort>()
            .And.Be(0);
    }

    [Fact(DisplayName = "A positive uint can be refined")]
    public static void Case19()
    {
        PositiveUInt refined = 1;
        ((uint)refined).Should().Be(1);
        Refined.TryRefine<uint, Positive>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A negative uint cannot be refined")]
    public static void Case20()
    {
        Refined.TryRefine<uint, Positive>(0, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A negative uint cannot be refined and throws")]
    public static void Case21()
    {
        var act = () => (PositiveUInt)0;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be positive, but found 0")
            .And.Value.Should()
            .BeOfType<uint>()
            .And.Be(0);
    }

    [Fact(DisplayName = "A positive ulong can be refined")]
    public static void Case22()
    {
        PositiveULong refined = 1;
        ((ulong)refined).Should().Be(1);
        Refined.TryRefine<ulong, Positive>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A negative ulong cannot be refined")]
    public static void Case23()
    {
        Refined.TryRefine<ulong, Positive>(0, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A negative ulong cannot be refined and throws")]
    public static void Case24()
    {
        var act = () => (PositiveULong)0;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be positive, but found 0")
            .And.Value.Should()
            .BeOfType<ulong>()
            .And.Be(0);
    }

    [Fact(DisplayName = "A positive float can be refined")]
    public static void Case25()
    {
        PositiveFloat refined = 1;
        ((float)refined).Should().Be(1);
        Refined.TryRefine<float, Positive>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A negative float cannot be refined")]
    public static void Case26()
    {
        Refined.TryRefine<float, Positive>(0, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A negative float cannot be refined and throws")]
    public static void Case27()
    {
        var act = () => (PositiveFloat)0;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be positive, but found 0")
            .And.Value.Should()
            .BeOfType<float>()
            .And.Be(0);
    }

    [Fact(DisplayName = "A positive double can be refined")]
    public static void Case28()
    {
        PositiveDouble refined = 1;
        ((double)refined).Should().Be(1);
        Refined.TryRefine<double, Positive>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A negative double cannot be refined")]
    public static void Case29()
    {
        Refined.TryRefine<double, Positive>(0, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A negative double cannot be refined and throws")]
    public static void Case30()
    {
        var act = () => (PositiveDouble)0;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be positive, but found 0")
            .And.Value.Should()
            .BeOfType<double>()
            .And.Be(0);
    }

    [Fact(DisplayName = "A positive decimal can be refined")]
    public static void Case31()
    {
        PositiveDecimal refined = 1;
        ((decimal)refined).Should().Be(1);
        Refined.TryRefine<decimal, Positive>(1, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A negative decimal cannot be refined")]
    public static void Case32()
    {
        Refined.TryRefine<decimal, Positive>(0, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A negative decimal cannot be refined and throws")]
    public static void Case33()
    {
        var act = () => (PositiveDecimal)0;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be positive, but found 0")
            .And.Value.Should()
            .BeOfType<decimal>()
            .And.Be(0);
    }
}
