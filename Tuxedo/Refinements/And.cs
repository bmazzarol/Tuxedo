namespace Tuxedo;

/// <summary>
/// Combines two refinements with a logical AND
/// </summary>
/// <typeparam name="TFirstRefinement">first refinement</typeparam>
/// <typeparam name="TSecondRefinement">second refinement</typeparam>
/// <typeparam name="T">type of value</typeparam>
public readonly struct And<T, TFirstRefinement, TSecondRefinement>
    : IRefinement<And<T, TFirstRefinement, TSecondRefinement>, T>
    where TFirstRefinement : struct, IRefinement<TFirstRefinement, T>
    where TSecondRefinement : struct, IRefinement<TSecondRefinement, T>
{
    /// <inheritdoc />
    public bool CanBeRefined(T value)
    {
        var firstRefinement = default(TFirstRefinement);
        var secondRefinement = default(TSecondRefinement);
        return firstRefinement.CanBeRefined(value) && secondRefinement.CanBeRefined(value);
    }

    /// <inheritdoc />
    public string BuildFailureMessage(T value)
    {
        var firstRefinement = default(TFirstRefinement);
        var secondRefinement = default(TSecondRefinement);
        return $"{firstRefinement.BuildFailureMessage(value)} and {secondRefinement.BuildFailureMessage(value)}";
    }
}
