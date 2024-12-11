using System.Numerics;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a <see cref="INumber{TSelf}"/> to be positive
/// </summary>
/// <typeparam name="T">type of the number</typeparam>
public readonly struct Positive<T> : IRefinement<Positive<T>, T>
    where T : INumber<T>
{
    static Positive<T> IRefinement<Positive<T>, T>.Value { get; }

    bool IRefinement<Positive<T>, T>.IsRefined(T value)
    {
        return value > T.Zero;
    }

    string IRefinement<Positive<T>, T>.BuildFailureMessage(T value)
    {
        return $"Value must be positive, but was '{value}'";
    }
}
