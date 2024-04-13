using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Ensures that a numeric value is negative
/// </summary>
public readonly struct Negative
    : ISignedNumericRefinement<Negative>,
        IFloatingPointNumericRefinement<Negative>
{
    bool IRefinement<Negative, sbyte>.CanBeRefined(
        sbyte value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be negative, but found {value}";
        return false;
    }

    bool IRefinement<Negative, short>.CanBeRefined(
        short value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be negative, but found {value}";
        return false;
    }

    bool IRefinement<Negative, int>.CanBeRefined(
        int value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be negative, but found {value}";
        return false;
    }

    bool IRefinement<Negative, long>.CanBeRefined(
        long value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be negative, but found {value}";
        return false;
    }

    bool IRefinement<Negative, float>.CanBeRefined(
        float value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be negative, but found {value}";
        return false;
    }

    bool IRefinement<Negative, double>.CanBeRefined(
        double value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be negative, but found {value}";
        return false;
    }

    bool IRefinement<Negative, decimal>.CanBeRefined(
        decimal value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be negative, but found {value}";
        return false;
    }
}
