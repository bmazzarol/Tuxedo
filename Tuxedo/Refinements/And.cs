namespace Tuxedo;

/// <summary>
/// Combines two refinements with a logical AND
/// </summary>
/// <typeparam name="TFirstRefinement">first refinement</typeparam>
/// <typeparam name="TSecondRefinement">second refinement</typeparam>
public readonly struct And<TFirstRefinement, TSecondRefinement>
    : IRefinement<And<TFirstRefinement, TSecondRefinement>>
    where TFirstRefinement : struct, IRefinement<TFirstRefinement>
    where TSecondRefinement : struct, IRefinement<TSecondRefinement>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value)
    {
        var firstRefinement = default(TFirstRefinement);
        var secondRefinement = default(TSecondRefinement);
        return firstRefinement.CanBeRefined(value) && secondRefinement.CanBeRefined(value);
    }

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value)
    {
        var firstRefinement = default(TFirstRefinement);
        var secondRefinement = default(TSecondRefinement);
        return $"{firstRefinement.BuildFailureMessage(value)} and {secondRefinement.BuildFailureMessage(value)}";
    }
}
