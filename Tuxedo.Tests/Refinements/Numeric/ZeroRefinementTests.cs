using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using ZeroByte = Raw<byte>.Refined<Zero<byte>>;

public sealed class ZeroRefinementTests
{
    [Fact(DisplayName = "A zero byte can be refined")]
    public void Case1()
    {
        var refined = (ZeroByte)0;
        refined.Value.Should().Be(0);

        ZeroByte.TryParse(0, out var refinedValue, out var failureMessage).Should().BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "A non-zero byte cannot be refined")]
    public void Case2()
    {
        var op = () => (ZeroByte)1;
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be zero, but was '1'");
        ZeroByte.TryParse(1, out _, out var failureMessage).Should().BeFalse();
        failureMessage.Should().Be("Value must be zero, but was '1'");
    }
}
