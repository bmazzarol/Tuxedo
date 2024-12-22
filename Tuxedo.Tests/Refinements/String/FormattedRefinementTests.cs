using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using GuidString = Raw<string>.Produces<Guid>.Refined<Formatted<Guid>>;

using DateOnlyString = Raw<string>.Produces<DateOnly>.Refined<Formatted<DateOnly>>;

public sealed class FormattedRefinementTests
{
    [Fact(DisplayName = "Formatted refinement refines a string to a guid in one pass")]
    public void Case1()
    {
        var refined = (GuidString)"CD3359BD-ADAC-4230-9CA9-CDCD23E5992A";
        refined.RawValue.Should().Be("CD3359BD-ADAC-4230-9CA9-CDCD23E5992A");
        refined.RefinedValue.Should().Be(new Guid("CD3359BD-ADAC-4230-9CA9-CDCD23E5992A"));

        GuidString
            .TryParse("CD3359BD-ADAC-4230-9CA9-CDCD23E5992A", out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }

    [Fact(
        DisplayName = "Formatted refinement does not refine a string to a guid"
    )]
    public void Case2()
    {
        var op = () => (GuidString)"not a guid";
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be a valid Guid, but was 'not a guid'");
        GuidString.TryParse("not a guid", out _, out var failureMessage).Should().BeFalse();
        failureMessage.Should().Be("Value must be a valid Guid, but was 'not a guid'");
    }
    
    [Fact(DisplayName = "Formatted refinement refines a string to a date only in one pass")]
    public void Case3()
    {
        var refined = (DateOnlyString)"2022-01-01";
        refined.RawValue.Should().Be("2022-01-01");
        refined.RefinedValue.Should().Be(new DateOnly(2022, 1, 1));
        
        string value = refined; // implicit conversion
        value.Should().Be("2022-01-01");
        DateOnly dateOnly = refined;
        dateOnly.Should().Be(new DateOnly(2022, 1, 1));

        DateOnlyString
            .TryParse("2022-01-01", out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }
}