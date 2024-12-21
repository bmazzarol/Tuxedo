using System.Collections;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a value to be non-empty
/// </summary>
public readonly struct NonEmpty : IRefinement<NonEmpty, IEnumerable>
{
    static NonEmpty IRefinement<NonEmpty, IEnumerable>.Value { get; }

    bool IRefinement<NonEmpty, IEnumerable>.IsRefined(IEnumerable value)
    {
        switch (value)
        {
            case string s:
                return !string.IsNullOrEmpty(s);
            case ICollection collection:
                return collection.Count > 0;
            default:
            {
                var enumerator = value.GetEnumerator();
                var result = enumerator.MoveNext();
                if (enumerator is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                return result;
            }
        }
    }

    string IRefinement<NonEmpty, IEnumerable>.BuildFailureMessage(IEnumerable value)
    {
        return "Value must be non-empty";
    }
}
