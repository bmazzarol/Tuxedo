using System.Collections;

namespace Tuxedo;

/// <summary>
/// Enforces that a value has a specific size
/// </summary>
/// <typeparam name="TSize">size refinement</typeparam>
public readonly struct Size<TSize> : IRefinement<Size<TSize>, IEnumerable>
    where TSize : struct, IRefinement<TSize, int>
{
    /// <inheritdoc />
    public bool CanBeRefined(IEnumerable value)
    {
        switch (value)
        {
            case ICollection collection:
                return default(TSize).CanBeRefined(collection.Count);
            default:
            {
                var count = 0;
                foreach (var _ in value)
                    count++;
                return default(TSize).CanBeRefined(count);
            }
        }
    }

    /// <inheritdoc />
    public string BuildFailureMessage(IEnumerable value) =>
        $"The values size failed refinement: {default(TSize).BuildFailureMessage(default)}";
}
