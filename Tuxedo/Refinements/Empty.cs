using System.Collections;

namespace Tuxedo;

/// <summary>
/// Enforces that a value is empty
/// </summary>
public readonly struct Empty : IRefinement<Empty>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value)
    {
        switch (value)
        {
            case null:
                return true;
            case string text:
                return string.IsNullOrEmpty(text);
            case ICollection collection:
                return collection.Count == 0;
            case IEnumerable enumerable:
            {
                var enumerator = enumerable.GetEnumerator();
                using var disposable = enumerator as IDisposable;
                return !enumerator.MoveNext();
            }
            default:
                return EqualityComparer<T>.Default.Equals(value, default!);
        }
    }

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value) => "Value must be empty";
}
