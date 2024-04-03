using System.Collections;

namespace Tuxedo;

/// <summary>
/// Enforces that a value is not empty
/// </summary>
public readonly struct NonEmpty : IRefinement<NonEmpty, IEnumerable>
{
    bool IRefinement<NonEmpty, IEnumerable>.CanBeRefined(IEnumerable value)
    {
        switch (value)
        {
            case null:
                return false;
            case ICollection collection:
                return collection.Count > 0;
        }

        var enumerator = value.GetEnumerator();
        using var disposable = enumerator as IDisposable;
        return enumerator.MoveNext();
    }

    string IRefinement<NonEmpty, IEnumerable>.BuildFailureMessage(IEnumerable value) =>
        "Value cannot be empty";
}
