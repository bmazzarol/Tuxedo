using System.Text.RegularExpressions;
using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using LocationCodeString = Raw<string>.Refined<Matching<LocationCodeRegex>>;

public readonly partial struct LocationCodeRegex : IConstant<LocationCodeRegex, Regex>
{
    [GeneratedRegex("^([1-9]{1})([0-9]{3})$", RegexOptions.Compiled, 100)]
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

        LocationCodeString
            .TryParse("1234", out var refinedValue, out var failureMessage)
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
            .WithMessage("Value '12345' does not match the regex '^([1-9]{1})([0-9]{3})$'");
        LocationCodeString.TryParse("12345", out _, out var failureMessage).Should().BeFalse();
        failureMessage
            .Should()
            .Be("Value '12345' does not match the regex '^([1-9]{1})([0-9]{3})$'");
    }

    [Fact(DisplayName = "Matches refinement captures and refines a string that matches the regex")]
    public void Case3()
    {
        var refined = (Raw<string>.Produces<MatchCollection>.Refined<Matching<LocationCodeRegex>>)
            "1234";
        refined.RefinedValue.Count.Should().Be(1);

        refined.RefinedValue[0].Groups.Count.Should().Be(3);

        refined.RefinedValue[0].Groups[1].Value.Should().Be("1");
        refined.RefinedValue[0].Groups[2].Value.Should().Be("234");
    }
}
