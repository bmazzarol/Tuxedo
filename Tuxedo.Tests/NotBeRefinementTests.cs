using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using NotZeroByte = Refined<NotBe<Zero<byte>, byte>, byte>;

public sealed class NotBeRefinementTests
{
    [Fact(DisplayName = "Not be can be used to negate a refinement")]
    public void Case1()
    {
        NotZeroByte refined = 1;
        refined.Value.Should().Be(1);

        Refined
            .TryRefine<NotBe<Zero<byte>, byte>, byte>(
                1,
                out var refinedValue,
                out var failureMessage
            )
            .Should()
            .BeTrue();
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
        Refined
            .TryRefine<NotBe<Zero<byte>, byte>, byte>(0, out _, out var failureMessage)
            .Should()
            .BeFalse();
        failureMessage.Should().Be("Value must not be zero, but was '0'");
    }
}
