using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Ensures that a numeric value is less than a given value
/// </summary>
/// <typeparam name="T">type of the value to compare</typeparam>
public readonly struct LessThan<T> : INumericRefinement<LessThan<T>>
    where T : struct, IConstant<T, long>
{
    bool IRefinement<LessThan<T>, sbyte>.CanBeRefined(
        sbyte value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < default(T).Value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be less than {default(T).Value}, but found {value}";
        return false;
    }

    bool IRefinement<LessThan<T>, short>.CanBeRefined(
        short value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < default(T).Value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be less than {default(T).Value}, but found {value}";
        return false;
    }

    bool IRefinement<LessThan<T>, int>.CanBeRefined(
        int value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < default(T).Value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be less than {default(T).Value}, but found {value}";
        return false;
    }

    bool IRefinement<LessThan<T>, long>.CanBeRefined(
        long value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < default(T).Value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be less than {default(T).Value}, but found {value}";
        return false;
    }

    bool IRefinement<LessThan<T>, byte>.CanBeRefined(
        byte value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < default(T).Value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be less than {default(T).Value}, but found {value}";
        return false;
    }

    bool IRefinement<LessThan<T>, ushort>.CanBeRefined(
        ushort value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < default(T).Value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be less than {default(T).Value}, but found {value}";
        return false;
    }

    bool IRefinement<LessThan<T>, uint>.CanBeRefined(
        uint value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < default(T).Value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be less than {default(T).Value}, but found {value}";
        return false;
    }

    bool IRefinement<LessThan<T>, ulong>.CanBeRefined(
        ulong value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < (ulong)default(T).Value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be less than {default(T).Value}, but found {value}";
        return false;
    }

    bool IRefinement<LessThan<T>, float>.CanBeRefined(
        float value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < default(T).Value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be less than {default(T).Value}, but found {value}";
        return false;
    }

    bool IRefinement<LessThan<T>, double>.CanBeRefined(
        double value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < default(T).Value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be less than {default(T).Value}, but found {value}";
        return false;
    }

    bool IRefinement<LessThan<T>, decimal>.CanBeRefined(
        decimal value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value < default(T).Value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be less than {default(T).Value}, but found {value}";
        return false;
    }
}
