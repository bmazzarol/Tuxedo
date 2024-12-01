using System.Diagnostics.CodeAnalysis;
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
    public override bool CanBeRefined(T value, [NotNullWhen(false)] out string? failureMessage)
    {
        if (value < T.Zero)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be negative, but was {value}";
        return false;
    }
}
