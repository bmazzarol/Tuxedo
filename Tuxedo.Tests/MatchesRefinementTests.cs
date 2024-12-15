using System.Text.RegularExpressions;
using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using LocationCodeString = Refined<Matches<LocationCodeRegex>, string>;

public readonly partial struct LocationCodeRegex : IConstant<LocationCodeRegex, Regex>
{
    [GeneratedRegex("^[1-9]{1}[0-9]{3}$", RegexOptions.Compiled, 100)]
    private static partial Regex LocationCode();

    static Regex IConstant<LocationCodeRegex, Regex>.Value => LocationCode();
}

public sealed class MatchesRefinementTests
{
    [Fact(DisplayName = "Matches refinement refines a string that matches the regex")]
    public void Case1()
    {
        var refined = (LocationCodeString)"1234";
        refined.Value.Should().Be("1234");

        Refined
            .TryRefine<Matches<LocationCodeRegex>, string>(
                "1234",
                out var refinedValue,
                out var failureMessage
            )
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }

    [Fact(
        DisplayName = "Matches refinement does not refine a string that does not match the regex"
    )]
    public void Case2()
    {
        var op = () => (LocationCodeString)"12345";
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value '12345' does not match the regex '^[1-9]{1}[0-9]{3}$'");
        Refined
            .TryRefine<Matches<LocationCodeRegex>, string>("12345", out _, out var failureMessage)
            .Should()
            .BeFalse();
        failureMessage.Should().Be("Value '12345' does not match the regex '^[1-9]{1}[0-9]{3}$'");
    }
}
