using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Combines two refinements with a logical AND
/// </summary>
/// <typeparam name="TFirstRefinement">first refinement</typeparam>
/// <typeparam name="TSecondRefinement">second refinement</typeparam>
/// <typeparam name="T">type to refine</typeparam>
public readonly struct And<TFirstRefinement, TSecondRefinement, T>
    : IRefinement<And<TFirstRefinement, TSecondRefinement, T>, T>
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
    public bool TryApplyRefinement(T value, [NotNullWhen(true)] out T? refinedValue)
    {
        var firstRefinement = default(TFirstRefinement);
        var secondRefinement = default(TSecondRefinement);

        if (
            firstRefinement.TryApplyRefinement(value, out var firstRefinedValue)
            && secondRefinement.TryApplyRefinement(firstRefinedValue, out var secondRefinedValue)
        )
        {
            refinedValue = secondRefinedValue;
            return true;
        }

        refinedValue = value;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(T value)
    {
        var firstRefinement = default(TFirstRefinement);
        var secondRefinement = default(TSecondRefinement);
        return $"{firstRefinement.BuildFailureMessage(value)} and {secondRefinement.BuildFailureMessage(value)}";
    }
}
