using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a value has a specific size
/// </summary>
/// <typeparam name="T">type of the value</typeparam>
/// <typeparam name="TSize">size refinement</typeparam>
public readonly struct Size<T, TSize> : IRefinement<Size<T, TSize>, T>
    where TSize : struct, IRefinement<TSize, int>
{
    /// <inheritdoc />
    public bool CanBeRefined(T value)
    {
        switch (value)
        {
            case ICollection collection:
                return default(TSize).CanBeRefined(collection.Count);
            case IEnumerable enumerable:
            {
                var count = enumerable.Cast<object?>().Count();
                return default(TSize).CanBeRefined(count);
            }
            default:
                return false;
        }
    }

    /// <inheritdoc />
    public bool TryApplyRefinement(T value, [NotNullWhen(true)] out T? refinedValue)
    {
        refinedValue = default;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(T value) =>
        $"The values size failed refinement: {default(TSize).BuildFailureMessage(default)}";
}
