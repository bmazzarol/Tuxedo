using FluentAssertions;
using Tuxedo.Tests.Extensions;

namespace Tuxedo.Tests;

#region DateOnlyExample

/// <summary>
/// This is a refined string that must be a valid date
/// </summary>
public readonly partial struct DateOnlyString
{
    [Refinement("The value must be a valid date, but was '{value}'")]
    private static bool DateOnly(string value, out DateOnly dateOnly) =>
        System.DateOnly.TryParse(value, out dateOnly);

    /// <summary>
    /// Some custom function that operates on the refined type
    /// </summary>
    public bool IsWeekend => RefinedValue.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
}

#endregion

public class DateOnlyExample
{
    #region DateOnlyStringUsage

    [Fact(DisplayName = "A string can be refined as a date")]
    public void Case1()
    {
        var refined = (DateOnlyString)"2021-02-28";
        // access the raw string
        refined.RawValue.Should().Be("2021-02-28");
        // access the DateOnly value that was produced from the refinement process
        refined.RefinedValue.Should().Be(DateOnly.Parse("2021-02-28"));
        // they can also be split out like this
        (string str, DateOnly dte) = refined;

        refined.IsWeekend.Should().Be(true);

        DateOnlyString
            .TryParse("2021-02-28", out var dateOnlyString, out var failureMessage)
            .Should()
            .BeTrue();
        failureMessage.Should().BeNull();
        (refined == dateOnlyString).Should().BeTrue();
    }

    [Fact(DisplayName = "A invalid date string cannot be refined as a date")]
    public void Case2()
    {
        Assert
            .Throws<InvalidOperationException>(() => (DateOnlyString)"not a date")
            .Message.Should()
            .Be("The value must be a valid date, but was 'not a date'");
        DateOnlyString.TryParse("not a date", out var refined, out var message).Should().BeFalse();
        message.Should().Be("The value must be a valid date, but was 'not a date'");
        refined.Should().Be(default(DateOnlyString));
    }

    #endregion


    [Fact(DisplayName = "DateOnlyString refinement snapshot is correct")]
    public Task Case3()
    {
        return """
            [Refinement("The value must be a valid date, but was '{value}'")]
            private static bool DateOnly(string value, out DateOnly dateOnly) =>
                System.DateOnly.TryParse(value, out dateOnly);
            """.VerifyRefinement();
    }
}
