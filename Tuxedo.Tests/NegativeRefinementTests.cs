using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using NegativeFloat = Refined<Negative<float>, float>;

public class NegativeRefinementTests
{
    [Fact(DisplayName = "A negative float can be refined")]
    public void Case1()
    {
        NegativeFloat refined = -1.0f;
        refined.Value.Should().Be(-1.0f);

        refined = Negative<float>.Refine(-2.0f);
        refined.Value.Should().Be(-2.0f);

        Refined
            .TryRefine<Negative<float>, float>(-2.0f, out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();

        Negative<float>.TryRefine(-2.0f, out refinedValue, out failureMessage).Should().BeTrue();
    }

    [Fact(DisplayName = "A positive float cannot be refined")]
    public void Case2()
    {
        var op = () => (NegativeFloat)(1.0f);
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be negative, but was '1'");
        Refined
            .TryRefine<Negative<float>, float>(1.0f, out _, out var failureMessage)
            .Should()
            .BeFalse();
        failureMessage.Should().Be("Value must be negative, but was '1'");
    }
}
