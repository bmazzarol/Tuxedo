using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using ExactlyFiveMinutes = Refined<Equal<TimeSpan, FiveMinutes>, TimeSpan>;

public sealed class FiveMinutes : Constant<FiveMinutes, TimeSpan>
{
    public override TimeSpan Value => TimeSpan.FromMinutes(5);
}

public sealed class EqualRefinementTests
{
    [Fact(DisplayName = "A time span of five minutes can be refined")]
    public void Case1()
    {
        ExactlyFiveMinutes refined = TimeSpan.FromMinutes(5);
        refined.Value.Should().Be(TimeSpan.FromMinutes(5));

        refined = Equal<TimeSpan, FiveMinutes>.Refine(TimeSpan.FromMinutes(5));
        refined.Value.Should().Be(TimeSpan.FromMinutes(5));

        Refined
            .TryRefine<Equal<TimeSpan, FiveMinutes>, TimeSpan>(
                TimeSpan.FromMinutes(5),
                out var refinedValue,
                out var failureMessage
            )
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "A time span of six minutes cannot be refined")]
    public void Case2()
    {
        var op = () => (ExactlyFiveMinutes)TimeSpan.FromMinutes(6);
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be equal to '00:05:00', but was '00:06:00'");
        Refined
            .TryRefine<Equal<TimeSpan, FiveMinutes>, TimeSpan>(
                TimeSpan.FromMinutes(6),
                out _,
                out var failureMessage
            )
            .Should()
            .BeFalse();
        failureMessage.Should().Be("Value must be equal to '00:05:00', but was '00:06:00'");
    }
}
