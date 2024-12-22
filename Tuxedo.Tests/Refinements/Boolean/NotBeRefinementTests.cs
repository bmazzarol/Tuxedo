using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using NotZeroByte = Raw<byte>.Refined<NotBe<Zero<byte>, byte>>;

public sealed class NotBeRefinementTests
{
    [Fact(DisplayName = "Not be can be used to negate a refinement")]
    public void Case1()
    {
        var refined = (NotZeroByte)1;
        refined.Value.Should().Be(1);

        NotZeroByte.TryParse(1, out var refinedValue, out var failureMessage).Should().BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "Not be can be used to negate a refinement that fails")]
    public void Case2()
    {
        var op = () => (NotZeroByte)0;
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must not be zero, but was '0'");
        NotZeroByte.TryParse(0, out _, out var failureMessage).Should().BeFalse();
        failureMessage.Should().Be("Value must not be zero, but was '0'");
    }
}
