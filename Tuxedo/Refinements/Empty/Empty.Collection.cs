using System.Collections;

namespace Tuxedo;

/// <summary>
/// Enforces that a value is empty
/// </summary>
public readonly struct Empty : IRefinement<Empty, IEnumerable>
{
    bool IRefinement<Empty, IEnumerable>.CanBeRefined(IEnumerable? value)
    {
        switch (value)
        {
            case null:
                return true;
            case ICollection collection:
                return collection.Count == 0;
        }

        var enumerator = value.GetEnumerator();
        using var disposable = enumerator as IDisposable;
        return !enumerator.MoveNext();
    }

    string IRefinement<Empty, IEnumerable>.BuildFailureMessage(IEnumerable value) =>
        "Value must be empty";
}
