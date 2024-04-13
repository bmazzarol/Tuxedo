using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Ensures that a numeric value is positive
/// </summary>
public readonly struct Positive : INumericRefinement<Positive>
{
    bool IRefinement<Positive, sbyte>.CanBeRefined(
        sbyte value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value > 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be positive, but found {value}";
        return false;
    }

    bool IRefinement<Positive, short>.CanBeRefined(
        short value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value > 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be positive, but found {value}";
        return false;
    }

    bool IRefinement<Positive, int>.CanBeRefined(
        int value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value > 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be positive, but found {value}";
        return false;
    }

    bool IRefinement<Positive, long>.CanBeRefined(
        long value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value > 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be positive, but found {value}";
        return false;
    }

    bool IRefinement<Positive, byte>.CanBeRefined(
        byte value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value > 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be positive, but found {value}";
        return false;
    }

    bool IRefinement<Positive, ushort>.CanBeRefined(
        ushort value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value > 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be positive, but found {value}";
        return false;
    }

    bool IRefinement<Positive, uint>.CanBeRefined(
        uint value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value > 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be positive, but found {value}";
        return false;
    }

    bool IRefinement<Positive, ulong>.CanBeRefined(
        ulong value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value > 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be positive, but found {value}";
        return false;
    }

    bool IRefinement<Positive, float>.CanBeRefined(
        float value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value > 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be positive, but found {value}";
        return false;
    }

    bool IRefinement<Positive, double>.CanBeRefined(
        double value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value > 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be positive, but found {value}";
        return false;
    }

    bool IRefinement<Positive, decimal>.CanBeRefined(
        decimal value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value > 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be positive, but found {value}";
        return false;
    }
}
