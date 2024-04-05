using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a value is not empty
/// </summary>
public readonly struct NonEmpty<T, TComparer> : IRefinement<NonEmpty<T, TComparer>, T>
    where TComparer : struct, IConstant<TComparer, IEqualityComparer<T?>>
{
    bool IRefinement<NonEmpty<T, TComparer>, T>.CanBeRefined(
        T value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (!default(TComparer).Value.Equals(value, default))
        {
            failureMessage = null;
            return true;
        }

        failureMessage = "Value cannot be empty";
        return false;
    }
}

/// <summary>
/// Enforces that a value is not empty
/// </summary>
public readonly struct NonEmpty<T> : IRefinement<NonEmpty<T>, T>
{
    bool IRefinement<NonEmpty<T>, T>.CanBeRefined(
        T value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (!default(DefaultComparer<T?>).Value!.Equals(value, default))
        {
            failureMessage = null;
            return true;
        }

        failureMessage = "Value cannot be empty";
        return false;
    }
}

/// <summary>
/// Default comparer for a type
/// </summary>
/// <typeparam name="T">type</typeparam>
public readonly struct DefaultComparer<T> : IConstant<DefaultComparer<T>, IEqualityComparer<T?>>
{
    /// <inheritdoc />
    public IEqualityComparer<T?> Value => EqualityComparer<T?>.Default;
}
