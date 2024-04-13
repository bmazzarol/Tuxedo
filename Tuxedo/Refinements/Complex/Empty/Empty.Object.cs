using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a value is empty
/// </summary>
public readonly struct Empty<T, TComparer> : IRefinement<Empty<T, TComparer>, T>
    where TComparer : struct, IConstant<TComparer, IEqualityComparer<T?>>
{
    bool IRefinement<Empty<T, TComparer>, T>.CanBeRefined(
        T value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (default(TComparer).Value.Equals(value, default))
        {
            failureMessage = null;
            return true;
        }

        failureMessage = "Value must be empty";
        return false;
    }
}

/// <summary>
/// Enforces that a value is empty
/// </summary>
public readonly struct Empty<T> : IRefinement<Empty<T>, T>
{
    bool IRefinement<Empty<T>, T>.CanBeRefined(
        T value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (default(DefaultComparer<T?>).Value!.Equals(value, default))
        {
            failureMessage = null;
            return true;
        }

        failureMessage = "Value must be empty";
        return false;
    }
}
