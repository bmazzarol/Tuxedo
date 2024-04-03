namespace Tuxedo;

/// <summary>
/// Enforces that a value is not empty
/// </summary>
public readonly struct NonEmpty<T, TComparer> : IRefinement<NonEmpty<T, TComparer>, T>
    where TComparer : struct, IConstant<TComparer, IEqualityComparer<T?>>
{
    bool IRefinement<NonEmpty<T, TComparer>, T>.CanBeRefined(T value) =>
        !default(TComparer).Value.Equals(value, default);

    string IRefinement<NonEmpty<T, TComparer>, T>.BuildFailureMessage(T value) =>
        "Value cannot be empty";
}

/// <summary>
/// Enforces that a value is not empty
/// </summary>
public readonly struct NonEmpty<T> : IRefinement<NonEmpty<T>, T>
{
    bool IRefinement<NonEmpty<T>, T>.CanBeRefined(T value) =>
        !default(DefaultComparer<T?>).Value!.Equals(value, default);

    string IRefinement<NonEmpty<T>, T>.BuildFailureMessage(T value) => "Value cannot be empty";
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
