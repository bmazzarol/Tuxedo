using static Tuxedo.NaturalNumber;
using static Tuxedo.NaturalNumber.Operations;

namespace Tuxedo.Tests.Numeric;

public static class LessThanTests
{
    [Fact(DisplayName = "A sbyte less than 5 can be refined")]
    public static void Case1()
    {
        Refined<sbyte, LessThan<Five>> refined = 4;
        ((sbyte)refined).Should().Be(4);
        Refined.TryRefine<sbyte, LessThan<Five>>(4, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A sbyte greater than 5 cannot be refined")]
    public static void Case2()
    {
        Refined.TryRefine<sbyte, LessThan<Five>>(5, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A sbyte greater than 5 cannot be refined and throws")]
    public static void Case3()
    {
        var act = () => (Refined<sbyte, LessThan<Five>>)5;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be less than 5, but found 5")
            .And.Value.Should()
            .BeOfType<sbyte>()
            .And.Be(5);
    }

    [Fact(DisplayName = "A short less than 100 can be refined")]
    public static void Case4()
    {
        Refined<short, LessThan<OneHundred>> refined = 99;
        ((short)refined).Should().Be(99);
        Refined.TryRefine<short, LessThan<OneHundred>>(99, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A short greater than 100 cannot be refined")]
    public static void Case5()
    {
        Refined.TryRefine<short, LessThan<OneHundred>>(100, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A short greater than 100 cannot be refined and throws")]
    public static void Case6()
    {
        var act = () => (Refined<short, LessThan<OneHundred>>)101;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be less than 100, but found 101")
            .And.Value.Should()
            .BeOfType<short>()
            .And.Be(101);
    }

    #region LessThanInt

    [Fact(DisplayName = "A int less than 53 can be refined")]
    public static void Case7()
    {
        Refined<int, LessThan<FiftyThree>> refined = 52;
        ((int)refined).Should().Be(52);
        Refined.TryRefine<int, LessThan<FiftyThree>>(52, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A int greater than 53 cannot be refined")]
    public static void Case8()
    {
        Refined.TryRefine<int, LessThan<FiftyThree>>(53, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A int greater than 53 cannot be refined and throws")]
    public static void Case9()
    {
        var act = () => (Refined<int, LessThan<FiftyThree>>)54;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be less than 53, but found 54")
            .And.Value.Should()
            .BeOfType<int>()
            .And.Be(54);
    }

    #endregion

    [Fact(DisplayName = "A long less than 5 can be refined")]
    public static void Case10()
    {
        Refined<long, LessThan<Five>> refined = 4;
        ((long)refined).Should().Be(4);
        Refined.TryRefine<long, LessThan<Five>>(4, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A long greater than 5 cannot be refined")]
    public static void Case11()
    {
        Refined.TryRefine<long, LessThan<Five>>(5, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A long greater than 5 cannot be refined and throws")]
    public static void Case12()
    {
        var act = () => (Refined<long, LessThan<Five>>)5;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be less than 5, but found 5")
            .And.Value.Should()
            .BeOfType<long>()
            .And.Be(5);
    }

    [Fact(DisplayName = "A byte less than 5 can be refined")]
    public static void Case13()
    {
        Refined<byte, LessThan<Five>> refined = 4;
        ((byte)refined).Should().Be(4);
        Refined.TryRefine<byte, LessThan<Five>>(4, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A byte greater than 5 cannot be refined")]
    public static void Case14()
    {
        Refined.TryRefine<byte, LessThan<Five>>(5, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A byte greater than 5 cannot be refined and throws")]
    public static void Case15()
    {
        var act = () => (Refined<byte, LessThan<Five>>)5;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be less than 5, but found 5")
            .And.Value.Should()
            .BeOfType<byte>()
            .And.Be(5);
    }

    [Fact(DisplayName = "A ushort less than 100 can be refined")]
    public static void Case16()
    {
        Refined<ushort, LessThan<OneHundred>> refined = 99;
        ((ushort)refined).Should().Be(99);
        Refined.TryRefine<ushort, LessThan<OneHundred>>(99, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A ushort greater than 100 cannot be refined")]
    public static void Case17()
    {
        Refined.TryRefine<ushort, LessThan<OneHundred>>(100, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A ushort greater than 100 cannot be refined and throws")]
    public static void Case18()
    {
        var act = () => (Refined<ushort, LessThan<OneHundred>>)100;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be less than 100, but found 100")
            .And.Value.Should()
            .BeOfType<ushort>()
            .And.Be(100);
    }

    [Fact(DisplayName = "A uint less than 53 can be refined")]
    public static void Case19()
    {
        Refined<uint, LessThan<FiftyThree>> refined = 52;
        ((uint)refined).Should().Be(52);
        Refined.TryRefine<uint, LessThan<FiftyThree>>(52, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A uint greater than 53 cannot be refined")]
    public static void Case20()
    {
        Refined.TryRefine<uint, LessThan<FiftyThree>>(53, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A uint greater than 53 cannot be refined and throws")]
    public static void Case21()
    {
        var act = () => (Refined<uint, LessThan<FiftyThree>>)53;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be less than 53, but found 53")
            .And.Value.Should()
            .BeOfType<uint>()
            .And.Be(53);
    }

    [Fact(DisplayName = "A ulong less than 5 can be refined")]
    public static void Case22()
    {
        Refined<ulong, LessThan<Five>> refined = 4;
        ((ulong)refined).Should().Be(4);
        Refined.TryRefine<ulong, LessThan<Five>>(4, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A ulong greater than 5 cannot be refined")]
    public static void Case23()
    {
        Refined.TryRefine<ulong, LessThan<Five>>(5, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A ulong greater than 5 cannot be refined and throws")]
    public static void Case24()
    {
        var act = () => (Refined<ulong, LessThan<Five>>)5;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be less than 5, but found 5")
            .And.Value.Should()
            .BeOfType<ulong>()
            .And.Be(5);
    }

    [Fact(DisplayName = "A float less than 5 can be refined")]
    public static void Case25()
    {
        Refined<float, LessThan<Five>> refined = 4;
        ((float)refined).Should().Be(4);
        Refined.TryRefine<float, LessThan<Five>>(4, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A float greater than 5 cannot be refined")]
    public static void Case26()
    {
        Refined.TryRefine<float, LessThan<Five>>(5, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A float greater than 5 cannot be refined and throws")]
    public static void Case27()
    {
        var act = () => (Refined<float, LessThan<Five>>)5;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be less than 5, but found 5")
            .And.Value.Should()
            .BeOfType<float>()
            .And.Be(5);
    }

    [Fact(DisplayName = "A double less than 5 can be refined")]
    public static void Case28()
    {
        Refined<double, LessThan<Five>> refined = 4;
        ((double)refined).Should().Be(4);
        Refined.TryRefine<double, LessThan<Five>>(4, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A double greater than 5 cannot be refined")]
    public static void Case29()
    {
        Refined.TryRefine<double, LessThan<Five>>(5, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A double greater than 5 cannot be refined and throws")]
    public static void Case30()
    {
        var act = () => (Refined<double, LessThan<Five>>)5;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be less than 5, but found 5")
            .And.Value.Should()
            .BeOfType<double>()
            .And.Be(5);
    }

    [Fact(DisplayName = "A decimal less than 5 can be refined")]
    public static void Case31()
    {
        Refined<decimal, LessThan<Five>> refined = 4;
        ((decimal)refined).Should().Be(4);
        Refined.TryRefine<decimal, LessThan<Five>>(4, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A decimal greater than 5 cannot be refined")]
    public static void Case32()
    {
        Refined.TryRefine<decimal, LessThan<Five>>(5, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A decimal greater than 5 cannot be refined and throws")]
    public static void Case33()
    {
        var act = () => (Refined<decimal, LessThan<Five>>)5;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be less than 5, but found 5")
            .And.Value.Should()
            .BeOfType<decimal>()
            .And.Be(5);
    }

    [Fact(DisplayName = "A int less than 1000 can be refined")]
    public static void Case34()
    {
        Refined<int, LessThan<Multiply<OneHundred, Ten>>> refined = 999;
        ((int)refined).Should().Be(999);
        Refined.TryRefine<int, LessThan<Multiply<OneHundred, Ten>>>(999, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A int greater than 1000 cannot be refined")]
    public static void Case35()
    {
        Refined.TryRefine<int, LessThan<Multiply<OneHundred, Ten>>>(1000, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A int greater than 1000 cannot be refined and throws")]
    public static void Case36()
    {
        var act = () => (Refined<int, LessThan<Multiply<OneHundred, Ten>>>)1000;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be less than 1000, but found 1000")
            .And.Value.Should()
            .BeOfType<int>()
            .And.Be(1000);
    }
}
