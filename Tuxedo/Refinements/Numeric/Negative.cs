using System.Numerics;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a <see cref="INumber{TSelf}"/> to be negative
/// </summary>
/// <typeparam name="T">type of the number</typeparam>
public readonly struct Negative<T> : IRefinement<Negative<T>, T>
    where T : INumber<T>
{
    static Negative<T> IRefinement<Negative<T>, T>.Value { get; }

    bool IRefinement<Negative<T>, T>.IsRefined(T value)
    {
        return value < T.Zero;
    }

    string IRefinement<Negative<T>, T>.BuildFailureMessage(T value)
    {
        return $"Value must be negative, but was '{value}'";
    }
}
