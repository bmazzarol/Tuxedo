﻿using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a value is not empty
/// </summary>
public readonly struct NonEmpty<T> : IRefinement<NonEmpty<T>, T>
{
    /// <inheritdoc />
    public bool CanBeRefined(T value)
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
    public bool TryApplyRefinement(T value, [NotNullWhen(true)] out T? refinedValue)
    {
        refinedValue = default;
        return CanBeRefined(value);
    }

    /// <inheritdoc />
    public string BuildFailureMessage(T value)
    {
        return "Value cannot be empty";
    }
}
