using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a value is not empty
/// </summary>
public readonly struct NonEmpty : IRefinement<NonEmpty, IEnumerable>
{
    bool IRefinement<NonEmpty, IEnumerable>.CanBeRefined(
        IEnumerable? value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        bool nonEmpty;
        switch (value)
        {
            case null:
                nonEmpty = false;
                break;
            case ICollection collection:
                nonEmpty = collection.Count > 0;
                break;
            default:
            {
                var enumerator = value.GetEnumerator();
                using var disposable = enumerator as IDisposable;
                nonEmpty = enumerator.MoveNext();
                break;
            }
        }

        if (nonEmpty)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = "Value cannot be empty";
        return false;
    }
}
