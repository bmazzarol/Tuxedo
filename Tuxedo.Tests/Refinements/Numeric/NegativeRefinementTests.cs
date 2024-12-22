using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using NegativeFloat = Raw<float>.Refined<Negative<float>>;

public class NegativeRefinementTests
{
    [Fact(DisplayName = "A negative float can be refined")]
    public void Case1()
    {
        var refined = (NegativeFloat)(-1.0f);
        refined.Value.Should().Be(-1.0f);

        NegativeFloat
            .TryParse(-1.0f, out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "A positive float cannot be refined")]
    public void Case2()
    {
        var op = () => (NegativeFloat)1.0f;
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be negative, but was '1'");
        NegativeFloat.TryParse(1.0f, out _, out var failureMessage).Should().BeFalse();
        failureMessage.Should().Be("Value must be negative, but was '1'");
    }
}
