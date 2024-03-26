using System.Collections;

namespace Tuxedo;

/// <summary>
/// Enforces that a value is not empty
/// </summary>
public readonly struct NonEmpty : IRefinement<NonEmpty>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value)
    {
        switch (value)
        {
            case null:
                return false;
            case string text:
                return text.Length > 0;
            case ICollection collection:
                return collection.Count > 0;
            case IEnumerable enumerable:
            {
                var enumerator = enumerable.GetEnumerator();
                using var disposable = enumerator as IDisposable;
                return enumerator.MoveNext();
            }
            default:
                return !EqualityComparer<T>.Default.Equals(value, default!);
        }
    }

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value) => "Value cannot be empty";
}
