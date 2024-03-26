using System.Collections;

namespace Tuxedo;

/// <summary>
/// Enforces that a value has a specific size
/// </summary>
/// <typeparam name="TSize">size refinement</typeparam>
public readonly struct Size<TSize> : IRefinement<Size<TSize>>
    where TSize : struct, IRefinement<TSize>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value)
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
    public string BuildFailureMessage<T>(T value) =>
        $"The values size failed refinement: {default(TSize).BuildFailureMessage(default(int))}";
}
