namespace Tuxedo;

/// <summary>
/// Enforces that a collection is not empty
/// </summary>
/// <typeparam name="TEnumerable">collection type</typeparam>
/// <typeparam name="T">element type</typeparam>
public readonly struct NonEmpty<TEnumerable, T> : IRefinement<NonEmpty<TEnumerable, T>, TEnumerable>
    where TEnumerable : IEnumerable<T>
{
    /// <inheritdoc />
    public bool CanBeRefined(TEnumerable value)
    {
        return value switch
        {
            ICollection<T> collection => collection.Count > 0,
            _ => value.Any()
        };
    }

    /// <inheritdoc />
    public bool TryApplyRefinement(TEnumerable value, out TEnumerable refinedValue)
    {
        refinedValue = value;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(TEnumerable value)
    {
        return "Value cannot be empty";
    }
}
