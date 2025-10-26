using Tuxedo.Tests.Extensions;

namespace Tuxedo.Tests;

#region DateOnlyExample

/// <summary>
/// This is a refined string that must be a valid date
/// </summary>
public readonly partial struct DateOnlyString : IEquatable<DateOnlyString>
{
    [Refinement("The value must be a valid date, but was '{value}'")]
    private static bool DateOnly(string value, out DateOnly dateOnly) =>
        System.DateOnly.TryParse(value, out dateOnly);

    /// <summary>
    /// Some custom function that operates on the refined type
    /// </summary>
    public bool IsWeekend => AltValue.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
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
        Assert.Equal("2021-02-28", refined.Value);
        // access the DateOnly value that was produced from the refinement process
        Assert.Equal(new DateOnly(2021, 2, 28), refined.AltValue);
        // they can also be split out like this
        (string str, DateOnly dte) = refined;

        Assert.True(refined.IsWeekend);

        Assert.True(
            DateOnlyString.TryParse("2021-02-28", out var refined2, out var failureMessage)
        );
        Assert.Null(failureMessage);
        Assert.Equal(refined, refined2);
    }

    [Fact(DisplayName = "A invalid date string cannot be refined as a date")]
    public void Case2()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => (DateOnlyString)"not a date");
        Assert.StartsWith("The value must be a valid date, but was 'not a date'", ex.Message);
        Assert.False(DateOnlyString.TryParse("not a date", out var refined, out var message));
        Assert.Equal("The value must be a valid date, but was 'not a date'", message);
#pragma warning disable TUX001
        Assert.Equal(default(DateOnlyString), refined);
#pragma warning restore TUX001
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
