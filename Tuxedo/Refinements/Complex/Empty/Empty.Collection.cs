using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a value is empty
/// </summary>
public readonly struct Empty : IRefinement<Empty, IEnumerable>
{
    bool IRefinement<Empty, IEnumerable>.CanBeRefined(
        IEnumerable? value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        bool isEmpty;
        switch (value)
        {
            case null:
                isEmpty = true;
                break;
            case ICollection collection:
                isEmpty = collection.Count == 0;
                break;
            default:
            {
                var enumerator = value.GetEnumerator();
                using var disposable = enumerator as IDisposable;
                isEmpty = !enumerator.MoveNext();
                break;
            }
        }

        if (isEmpty)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = "Value must be empty";
        return false;
    }
}
