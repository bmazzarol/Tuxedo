using System.Numerics;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a <see cref="INumber{TSelf}"/> to be negative
/// </summary>
/// <typeparam name="T">type of the number</typeparam>
public sealed class Negative<T> : Refinement<Negative<T>, T>
    where T : INumber<T>
{
    /// <inheritdoc />
    protected override bool IsRefined(T value) => value < T.Zero;

    /// <inheritdoc />
    protected override string BuildFailureMessage(T value) =>
        $"Value must be negative, but was '{value}'";
}
