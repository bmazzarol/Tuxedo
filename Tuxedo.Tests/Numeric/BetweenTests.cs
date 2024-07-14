using static Tuxedo.NaturalNumber;

namespace Tuxedo.Tests.Numeric;

public sealed class BetweenTests
{
    [Fact(DisplayName = "A sbyte can be refined to be between two other sbytes")]
    public void Case1()
    {
        Refined<sbyte, Between<Two, Four>> refined = 2;
        ((sbyte)refined).Should().Be(2);
        Refined.TryRefine<sbyte, Between<Two, Four>>(2, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A sbyte cannot be refined to be between two other sbytes")]
    public void Case2()
    {
        Refined.TryRefine<sbyte, Between<Two, Four>>(1, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A sbyte cannot be refined to be between two other sbytes and throws")]
    public void Case3()
    {
        var act = () => (Refined<sbyte, Between<Two, Four>>)1;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Number must be between 2 and 4, but found 1")
            .And.Value.Should()
            .BeOfType<sbyte>()
            .And.Be(1);
    }
}
