using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Ensures that a numeric value is even
/// </summary>
public readonly struct Even : ISignedNumericRefinement<Even>, IUnsignedNumericRefinement<Even>
{
    bool IRefinement<Even, sbyte>.CanBeRefined(
        sbyte value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value % 2 == 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be an even number, but found {value}";
        return false;
    }

    bool IRefinement<Even, short>.CanBeRefined(
        short value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value % 2 == 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be an even number, but found {value}";
        return false;
    }

    bool IRefinement<Even, int>.CanBeRefined(
        int value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value % 2 == 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be an even number, but found {value}";
        return false;
    }

    bool IRefinement<Even, long>.CanBeRefined(
        long value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value % 2 == 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be an even number, but found {value}";
        return false;
    }

    bool IRefinement<Even, byte>.CanBeRefined(
        byte value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value % 2 == 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be an even number, but found {value}";
        return false;
    }

    bool IRefinement<Even, ushort>.CanBeRefined(
        ushort value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value % 2 == 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be an even number, but found {value}";
        return false;
    }

    bool IRefinement<Even, uint>.CanBeRefined(
        uint value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value % 2 == 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be an even number, but found {value}";
        return false;
    }

    bool IRefinement<Even, ulong>.CanBeRefined(
        ulong value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value % 2 == 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be an even number, but found {value}";
        return false;
    }
}
