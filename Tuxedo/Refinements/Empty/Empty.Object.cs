namespace Tuxedo;

/// <summary>
/// Enforces that a value is empty
/// </summary>
public readonly struct Empty<T, TComparer> : IRefinement<Empty<T, TComparer>, T>
    where TComparer : struct, IConstant<TComparer, IEqualityComparer<T?>>
{
    bool IRefinement<Empty<T, TComparer>, T>.CanBeRefined(T value) =>
        default(TComparer).Value.Equals(value, default);

    string IRefinement<Empty<T, TComparer>, T>.BuildFailureMessage(T value) =>
        "Value must be empty";
}

/// <summary>
/// Enforces that a value is empty
/// </summary>
public readonly struct Empty<T> : IRefinement<Empty<T>, T>
{
    bool IRefinement<Empty<T>, T>.CanBeRefined(T value) =>
        default(DefaultComparer<T?>).Value!.Equals(value, default);

    string IRefinement<Empty<T>, T>.BuildFailureMessage(T value) => "Value must be empty";
}
