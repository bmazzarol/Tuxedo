using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests.Refinements.Char;

using DigitChar = Raw<char>.Refined<Digit>;

public sealed class DigitRefinementTests
{
    [Fact(DisplayName = "A digit char can be refined")]
    public void Case1()
    {
        var refined = (DigitChar)'1';
        refined.Value.Should().Be('1');

        DigitChar.TryParse('1', out var refinedValue, out var failureMessage).Should().BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "A digit char cannot be refined")]
    public void Case2()
    {
        var op = () => (DigitChar)'a';
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Character must be a digit, but was 'a'");
        DigitChar.TryParse('a', out _, out var failureMessage).Should().BeFalse();
        failureMessage.Should().Be("Character must be a digit, but was 'a'");
    }
}
