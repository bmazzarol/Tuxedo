using System.Numerics;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a <see cref="INumber{TSelf}"/> to be <see cref="INumberBase{TSelf}.Zero"/>
/// </summary>
/// <typeparam name="T">type of the number</typeparam>
public readonly struct Zero<T> : IRefinement<Zero<T>, T>
    where T : INumber<T>
{
    static Zero<T> IRefinement<Zero<T>, T>.Value { get; }

    bool IRefinement<Zero<T>, T>.IsRefined(T value)
    {
        return value == T.Zero;
    }

    string IRefinement<Zero<T>, T>.BuildFailureMessage(T value)
    {
        return $"Value must be zero, but was '{value}'";
    }
}
