using System.Numerics;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a <see cref="INumber{TSelf}"/> to be positive
/// </summary>
/// <typeparam name="T">type of the number</typeparam>
public sealed class Positive<T> : Refinement<Positive<T>, T>
    where T : INumber<T>
{
    /// <inheritdoc />
    protected override bool IsRefined(T value) => value > T.Zero;

    /// <inheritdoc />
    protected override string BuildFailureMessage(T value) =>
        $"Value must be positive, but was '{value}'";
}
