using System.Diagnostics.CodeAnalysis;
using Tuxedo.Refinements;

namespace Tuxedo.Tests.Refinements;

using Template = Raw<string>.Produces<double>.IRefinedType<PositiveDoubleString, Formatted<double>>;

/// <summary>
/// This is an example refined type with an alternative value
/// </summary>
public readonly struct PositiveDoubleString : Template
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
        return Template.ParseInternal(value);
    }

    public static bool TryParse(
        string value,
        out PositiveDoubleString refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        return Template.TryParseInternal(value, out refined, out failureMessage);
    }
}
