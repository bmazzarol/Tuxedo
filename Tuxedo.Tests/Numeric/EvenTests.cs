namespace Tuxedo.Tests.Numeric;

public static class EvenTests
{
    [Fact(DisplayName = "A even sbyte can be refined")]
    public static void Case1()
    {
        EvenSByte refined = 2;
        ((sbyte)refined).Should().Be(2);
        Refined.TryRefine<sbyte, Even>(2, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A odd sbyte cannot be refined")]
    public static void Case2()
    {
        Refined.TryRefine<sbyte, Even>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A odd sbyte cannot be refined and throws")]
    public static void Case3()
    {
        var act = () => (EvenSByte)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be an even number, but found 1")
            .And.Value.Should()
            .BeOfType<sbyte>()
            .And.Be(1);
    }

    [Fact(DisplayName = "A even short can be refined")]
    public static void Case4()
    {
        EvenShort refined = 2;
        ((short)refined).Should().Be(2);
        Refined.TryRefine<short, Even>(2, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A odd short cannot be refined")]
    public static void Case5()
    {
        Refined.TryRefine<short, Even>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A odd short cannot be refined and throws")]
    public static void Case6()
    {
        var act = () => (EvenShort)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be an even number, but found 1")
            .And.Value.Should()
            .BeOfType<short>()
            .And.Be(1);
    }

    #region EvenInt

    [Fact(DisplayName = "A even int can be refined")]
    public static void Case7()
    {
        EvenInt refined = 2;
        ((int)refined).Should().Be(2);
        Refined.TryRefine<int, Even>(2, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A odd int cannot be refined")]
    public static void Case8()
    {
        Refined.TryRefine<int, Even>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A odd int cannot be refined and throws")]
    public static void Case9()
    {
        var act = () => (EvenInt)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be an even number, but found 1")
            .And.Value.Should()
            .BeOfType<int>()
            .And.Be(1);
    }

    #endregion

    [Fact(DisplayName = "A even long can be refined")]
    public static void Case10()
    {
        EvenLong refined = 2;
        ((long)refined).Should().Be(2);
        Refined.TryRefine<long, Even>(2, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A odd long cannot be refined")]
    public static void Case11()
    {
        Refined.TryRefine<long, Even>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A odd long cannot be refined and throws")]
    public static void Case12()
    {
        var act = () => (EvenLong)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be an even number, but found 1")
            .And.Value.Should()
            .BeOfType<long>()
            .And.Be(1);
    }

    [Fact(DisplayName = "A even byte can be refined")]
    public static void Case13()
    {
        EvenByte refined = 2;
        ((byte)refined).Should().Be(2);
        Refined.TryRefine<byte, Even>(2, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A odd byte cannot be refined")]
    public static void Case14()
    {
        Refined.TryRefine<byte, Even>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A odd byte cannot be refined and throws")]
    public static void Case15()
    {
        var act = () => (EvenByte)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be an even number, but found 1")
            .And.Value.Should()
            .BeOfType<byte>()
            .And.Be(1);
    }

    [Fact(DisplayName = "A even ushort can be refined")]
    public static void Case16()
    {
        EvenUShort refined = 2;
        ((ushort)refined).Should().Be(2);
        Refined.TryRefine<ushort, Even>(2, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A odd ushort cannot be refined")]
    public static void Case17()
    {
        Refined.TryRefine<ushort, Even>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A odd ushort cannot be refined and throws")]
    public static void Case18()
    {
        var act = () => (EvenUShort)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be an even number, but found 1")
            .And.Value.Should()
            .BeOfType<ushort>()
            .And.Be(1);
    }

    [Fact(DisplayName = "A even uint can be refined")]
    public static void Case19()
    {
        EvenUInt refined = 2;
        ((uint)refined).Should().Be(2);
        Refined.TryRefine<uint, Even>(2, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A odd uint cannot be refined")]
    public static void Case20()
    {
        Refined.TryRefine<uint, Even>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A odd uint cannot be refined and throws")]
    public static void Case21()
    {
        var act = () => (EvenUInt)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be an even number, but found 1")
            .And.Value.Should()
            .BeOfType<uint>()
            .And.Be(1);
    }

    [Fact(DisplayName = "A even ulong can be refined")]
    public static void Case22()
    {
        EvenULong refined = 2;
        ((ulong)refined).Should().Be(2);
        Refined.TryRefine<ulong, Even>(2, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A odd ulong cannot be refined")]
    public static void Case23()
    {
        Refined.TryRefine<ulong, Even>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A odd ulong cannot be refined and throws")]
    public static void Case24()
    {
        var act = () => (EvenULong)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be an even number, but found 1")
            .And.Value.Should()
            .BeOfType<ulong>()
            .And.Be(1);
    }
}
