using System.Diagnostics.CodeAnalysis;
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
    public override bool CanBeRefined(T value, [NotNullWhen(false)] out string? failureMessage)
    {
        if (value == T.Zero)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be zero, but was {value}";
        return false;
    }
}
