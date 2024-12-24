using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests.Refinements;

// if we know all the types, this cleans it up
using PositiveDoubleStringTemplate = Raw<string>.Produces<double>.IRefinedType<
    PositiveDoubleString,
    Formatted<double>
>;
using PositiveDoubleTemplate = Raw<double>.IRefinedType<PositiveDouble, Positive<double>>;

/// <summary>
/// An example custom refined type. The <see cref="Raw{T}.IRefinedType{TThis,TRefinement}"/> interface
/// can be used as a constructor to enable the use of structs as we have no inheritance.
/// </summary>
public readonly struct PositiveDouble : PositiveDoubleTemplate
{
    public double Value { get; }

    // this is required, otherwise it cannot be constructed, must be private
    private PositiveDouble(double value)
    {
        Value = value;
    }

    public static implicit operator double(PositiveDouble refinedValue)
    {
        return refinedValue.Value;
    }

    public static explicit operator PositiveDouble(double value)
    {
        return Parse(value);
    }

    public static PositiveDouble Parse(double value)
    {
        return PositiveDoubleTemplate.ParseInternal(value);
    }

    public static bool TryParse(
        double value,
        out PositiveDouble refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        return PositiveDoubleTemplate.TryParseInternal(value, out refined, out failureMessage);
    }
}

/// <summary>
/// This is an example refined type with an alternative value
/// </summary>
public readonly struct PositiveDoubleString : PositiveDoubleStringTemplate
{
    public string RawValue { get; }
    public double RefinedValue { get; }

    // this is required to construct the type if it passes the refinement, must be private
    private PositiveDoubleString(string rawValue, double refinedValue)
    {
        RawValue = rawValue;
        RefinedValue = refinedValue;
    }

    public static implicit operator string(PositiveDoubleString refinedValue)
    {
        return refinedValue.RawValue;
    }

    public static implicit operator double(PositiveDoubleString refinedValue)
    {
        return refinedValue.RefinedValue;
    }

    public static explicit operator PositiveDoubleString(string value)
    {
        return Parse(value);
    }

    public static PositiveDoubleString Parse(string value)
    {
        return PositiveDoubleStringTemplate.ParseInternal(value);
    }

    public static bool TryParse(
        string value,
        out PositiveDoubleString refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        return PositiveDoubleStringTemplate.TryParseInternal(
            value,
            out refined,
            out failureMessage
        );
    }
}

public class CustomRefinedTypeExample
{
    [Fact(DisplayName = "A positive double can be refined")]
    public void Case1()
    {
        var refined = (PositiveDouble)1.345;
        refined.Value.Should().Be(1.345);

        double value = refined;
        value.Should().Be(1.345);

        if (refined is { Value: 1.345 }) { }
        else
        {
            throw new Xunit.Sdk.XunitException("Pattern match failed");
        }

        PositiveDouble
            .TryParse(1.345, out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        refinedValue.Value.Should().Be(refined.Value);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "A negative double cannot be refined")]
    public void Case2()
    {
        var op = () => (PositiveDouble)(-1.345);
        var failure = op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be positive, but was '-1.345'")
            .Which;
        failure.Value.Should().Be(-1.345);
        failure.ValueType.Should().Be<double>();
        PositiveDouble.TryParse(-1.345, out _, out var failureMessage).Should().BeFalse();
        failureMessage.Should().Be("Value must be positive, but was '-1.345'");
    }

    [Fact(DisplayName = "A string can be refined as double")]
    public void Case3()
    {
        var refined = (PositiveDoubleString)"1.345";
        refined.RawValue.Should().Be("1.345");
        refined.RefinedValue.Should().Be(1.345);

        PositiveDoubleString
            .TryParse("1.345", out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "A string can not be refined as double")]
    public void Case4()
    {
        var op = () => (PositiveDoubleString)"not a double";
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be a valid Double, but was 'not a double'");
        PositiveDoubleString
            .TryParse("not a double", out _, out var failureMessage)
            .Should()
            .BeFalse();
        failureMessage.Should().Be("Value must be a valid Double, but was 'not a double'");
    }
}
