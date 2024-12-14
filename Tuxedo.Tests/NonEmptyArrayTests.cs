using FluentAssertions;
using Tuxedo.Types;
using Xunit;

namespace Tuxedo.Tests;

public sealed class NonEmptyArrayTests
{
    [Fact(DisplayName = "A non-empty array can be refined")]
    public void Case1()
    {
        NonEmptyArray<int> refined = new[] { 1, 2, 3 };
        refined.Value.Should().BeEquivalentTo([1, 2, 3]);

        int[] value = refined;
        value.Should().BeEquivalentTo([1, 2, 3]);

        refined.Head.Should().Be(1);
    }

    [Fact(DisplayName = "An empty array cannot be refined")]
    public void Case2()
    {
        var op = () => (NonEmptyArray<int>)Array.Empty<int>();
        var stackTrace = op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be non-empty")
            .Which.StackTrace;
        stackTrace.Should().Contain("NonEmptyArrayTests.Case2()");
    }
}
