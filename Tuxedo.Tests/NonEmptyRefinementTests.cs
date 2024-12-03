using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using NonEmptyString = Refined<NonEmpty<string>, string>;

public sealed class NonEmptyRefinementTests
{
    [Fact(DisplayName = "A non-empty string can be refined")]
    public void Case1()
    {
        NonEmptyString refined = "a";
        refined.Value.Should().Be("a");

        refined = NonEmpty<string>.Refine("b");
        refined.Value.Should().Be("b");

        Refined
            .TryRefine<NonEmpty<string>, string>("b", out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();

        NonEmpty<string>.TryRefine("b", out refinedValue, out failureMessage).Should().BeTrue();
    }

    [Fact(DisplayName = "An empty string cannot be refined")]
    public void Case2()
    {
        var op = () => (NonEmptyString)string.Empty;
        op.Should().Throw<RefinementFailureException>().WithMessage("Value must be non-empty");
        Refined
            .TryRefine<NonEmpty<string>, string>(string.Empty, out _, out var failureMessage)
            .Should()
            .BeFalse();
        failureMessage.Should().Be("Value must be non-empty");
    }

    [Fact(DisplayName = "A non-empty collection can be refined")]
    public void Case3()
    {
        var collection = new[] { 1, 2, 3 };
        var refined = NonEmpty<int[]>.Refine(collection);
        refined.Value.Should().BeEquivalentTo(collection);

        Refined
            .TryRefine<NonEmpty<int[]>, int[]>(
                collection,
                out var refinedValue,
                out var failureMessage
            )
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();

        NonEmpty<int[]>
            .TryRefine(collection, out refinedValue, out failureMessage)
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName = "A non-empty enumerable can be refined")]
    public void Case4()
    {
        var refined = NonEmpty<IEnumerable<int>>.Refine(Enumerable.Range(1, 3));
        refined.Value.Should().BeEquivalentTo(Enumerable.Range(1, 3));

        Refined
            .TryRefine<NonEmpty<IEnumerable<int>>, IEnumerable<int>>(
                Enumerable.Range(1, 3),
                out var refinedValue,
                out var failureMessage
            )
            .Should()
            .BeTrue();
        refinedValue.Should().NotBeNull();
        failureMessage.Should().BeNull();

        NonEmpty<IEnumerable<int>>
            .TryRefine(Enumerable.Range(1, 3), out refinedValue, out failureMessage)
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName = "A non null object can be refined")]
    public void Case5()
    {
        var refined = NonEmpty<object>.Refine(new object());
        refined.Value.Should().NotBeNull();

        Refined
            .TryRefine<NonEmpty<object>, object>(
                new object(),
                out var refinedValue,
                out var failureMessage
            )
            .Should()
            .BeTrue();
        refinedValue.Should().NotBeNull();
        failureMessage.Should().BeNull();

        NonEmpty<object>
            .TryRefine(new object(), out refinedValue, out failureMessage)
            .Should()
            .BeTrue();
    }
}
