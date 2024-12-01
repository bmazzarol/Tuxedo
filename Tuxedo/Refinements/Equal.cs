using System.Diagnostics.CodeAnalysis;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a value to be equal to a constant
/// </summary>
/// <typeparam name="T">type of the value</typeparam>
/// <typeparam name="TOther">type of the constant</typeparam>
public sealed class Equal<T, TOther> : Refinement<Equal<T, TOther>, T>
    where T : IComparable<T>
    where TOther : Constant<TOther, T>, new()
{
    /// <inheritdoc />
    public override bool CanBeRefined(T value, [NotNullWhen(false)] out string? failureMessage)
    {
        var other = Constant<TOther, T>.Value.ConstValue;
        if (value.Equals(other))
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be equal to {other}, but was {value}";
        return false;
    }
}
