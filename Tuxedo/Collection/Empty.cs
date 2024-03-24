using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a value is empty
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct Empty<T> : IRefinement<Empty<T>, T>
{
    /// <inheritdoc />
    public bool CanBeRefined(T value)
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
    public bool TryApplyRefinement(T value, [NotNullWhen(true)] out T? refinedValue)
    {
        refinedValue = default;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(T value)
    {
        return "Value must be empty";
    }
}
