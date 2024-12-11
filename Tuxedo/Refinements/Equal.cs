﻿namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a value to be equal to a constant
/// </summary>
/// <typeparam name="T">type of the value</typeparam>
/// <typeparam name="TOther">type of the constant</typeparam>
public sealed class Equal<T, TOther> : IRefinement<Equal<T, TOther>, T>
    where T : IComparable<T>
    where TOther : IConstant<TOther, T>, new()
{
    static IRefinement<Equal<T, TOther>, T> IRefinement<Equal<T, TOther>, T>.Value { get; } =
        new Equal<T, TOther>();

    bool IRefinement<Equal<T, TOther>, T>.IsRefined(T value)
    {
        return value.Equals(TOther.Value);
    }

    string IRefinement<Equal<T, TOther>, T>.BuildFailureMessage(T value)
    {
        return $"Value must be equal to '{TOther.Value}', but was '{value}'";
    }
}
