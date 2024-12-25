using System.Diagnostics.CodeAnalysis;
using Tuxedo.Refinements;

namespace Tuxedo.Tests;

using Template = Raw<double>.IRefinedType<PositiveDouble, Positive<double>>;

/// <summary>
/// An example custom refined type. The <see cref="Raw{T}.IRefinedType{TThis,TRefinement}"/> interface
/// can be used as a constructor to enable the use of structs as we have no inheritance.
/// </summary>
public readonly struct PositiveDouble : Template
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
        return Template.ParseInternal(value);
    }

    public static bool TryParse(
        double value,
        out PositiveDouble refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        return Template.TryParseInternal(value, out refined, out failureMessage);
    }
}
