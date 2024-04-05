using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a value has a specific size
/// </summary>
/// <typeparam name="TSize">size refinement</typeparam>
public readonly struct Size<TSize> : IRefinement<Size<TSize>, IEnumerable>
    where TSize : struct, IRefinement<TSize, int>
{
    /// <inheritdoc />
    public bool CanBeRefined(IEnumerable value, [NotNullWhen(false)] out string? failureMessage)
    {
        var size = value switch
        {
            ICollection collection => collection.Count,
            _ => value.Cast<object?>().Count()
        };

        if (default(TSize).CanBeRefined(size, out var sizeFailureMessage))
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"The values size failed refinement: {sizeFailureMessage}";
        return false;
    }
}
