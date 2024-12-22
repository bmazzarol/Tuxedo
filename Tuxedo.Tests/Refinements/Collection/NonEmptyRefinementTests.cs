using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using NonEmptyString = Raw<string>.Refined<NonEmpty>;

public sealed class NonEmptyRefinementTests
{
    [Fact(DisplayName = "A non-empty string can be refined")]
    public void Case1()
    {
        var refined = (NonEmptyString)"a";
        refined.Value.Should().Be("a");

        NonEmptyString
            .TryParse("a", out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "An empty string cannot be refined")]
    public void Case2()
    {
        var op = () => (NonEmptyString)string.Empty;
        op.Should().Throw<RefinementFailureException>().WithMessage("Value must be non-empty");
        NonEmptyString.TryParse(string.Empty, out _, out var failureMessage).Should().BeFalse();
        failureMessage.Should().Be("Value must be non-empty");
    }

    [Fact(DisplayName = "A non-empty collection can be refined")]
    public void Case3()
    {
        var collection = new[] { 1, 2, 3 };
        Raw<int[]>
            .Refined<NonEmpty>.TryParse(collection, out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        failureMessage.Should().BeNull();
        refinedValue.Value.Should().BeEquivalentTo(collection);
    }

    [Fact(DisplayName = "A non-empty enumerable can be refined")]
    public void Case4()
    {
        Raw<IEnumerable<int>>
            .Refined<NonEmpty>.TryParse(
                Enumerable.Range(1, 3),
                out var refinedValue,
                out var failureMessage
            )
            .Should()
            .BeTrue();
        refinedValue.Should().NotBeNull();
        failureMessage.Should().BeNull();
    }
}
