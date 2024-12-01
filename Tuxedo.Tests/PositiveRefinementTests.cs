using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using PositiveInt = Refined<Positive<int>, int>;

public class PositiveRefinementTests
{
    [Fact(DisplayName = "A positive integer can be refined")]
    public void Case1()
    {
        PositiveInt refined = 1;
        refined.Value.Should().Be(1);

        int value = refined;
        value.Should().Be(1);

        if (refined is { Value: 1 }) { }
        else
        {
            throw new Xunit.Sdk.XunitException("Pattern match failed");
        }

        refined = Positive<int>.Refine(2);
        refined.Value.Should().Be(2);

        Refined
            .TryRefine<Positive<int>, int>(2, out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "A negative integer cannot be refined")]
    public void Case2()
    {
        var op = () => (PositiveInt)(-1);
        var failure = op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be positive, but was -1")
            .Which;
        failure.Value.Should().Be(-1);
        failure.ValueType.Should().Be<int>();
        Refined.TryRefine<Positive<int>, int>(-1, out _, out var failureMessage).Should().BeFalse();
        failureMessage.Should().Be("Value must be positive, but was -1");
    }
}
