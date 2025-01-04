using FluentAssertions;

namespace Tuxedo.Tests;

#region DependentTypeExample

public readonly partial struct PositiveInt
{
    [Refinement("The value must be positive")]
    private static bool Positive(int value) => value > 0;
}

/// <summary>
/// This is a range type that is always composed correctly, its starting value
/// must always be less than its ending and will always produce a positive size.
/// </summary>
public readonly partial struct Range
{
    [Refinement(
        "The value must be a valid range where the start value is greater than the end value, '{value.End}' is not greater than '{value.Start}'",
        Name = nameof(Range)
    )]
    private static bool Predicate((int Start, int End) value) => value.Start < value.End;

    /// <summary>
    /// Size of the range
    /// </summary>
    public PositiveInt Size => PositiveInt.Parse(Value.End - Value.Start);
}

#endregion

public class SafeRangeExample
{
    [Fact(DisplayName = "A correct range can be refined")]
    public void Case1()
    {
        #region DependentTypeUsage

        var range = Range.Parse((Start: 1, End: 4));
        range.Size.Value.Should().Be(3);

        // the range can never be a negative size
        range = Range.Parse(
            (
                Start: -21,
                // must always be greater than Start
                End: -3
            )
        );

        range.Size.Value.Should().Be(18);

        // invalid ranges cannot exist
        Range.TryParse((Start: 6, End: 1), out _, out var message).Should().BeFalse();
        message
            .Should()
            .Be(
                "The value must be a valid range where the start value is greater than the end value, '1' is not greater than '6'"
            );

        #endregion
    }
}
