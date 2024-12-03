using System.Numerics;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a <see cref="INumber{TSelf}"/> to be <see cref="INumberBase{TSelf}.Zero"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Zero<T> : Refinement<Zero<T>, T>
    where T : INumber<T>
{
    /// <inheritdoc />
    protected override bool IsRefined(T value) => value == T.Zero;

    /// <inheritdoc />
    protected override string BuildFailureMessage(T value) =>
        $"Value must be zero, but was '{value}'";
}
