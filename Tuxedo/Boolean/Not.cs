using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Inverts a refinement
/// </summary>
/// <typeparam name="TRefinement">refinement to invert</typeparam>
/// <typeparam name="T">type to refine</typeparam>
public readonly struct Not<TRefinement, T> : IRefinement<Not<TRefinement, T>, T>
    where TRefinement : struct, IRefinement<TRefinement, T>
{
    /// <inheritdoc />
    public bool CanBeRefined(T value)
    {
        var refinement = default(TRefinement);
        return !refinement.CanBeRefined(value);
    }

    /// <inheritdoc />
    public bool TryApplyRefinement(T value, [NotNullWhen(true)] out T? refinedValue)
    {
        refinedValue = value;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(T value)
    {
        var refinement = default(TRefinement);
        return $"Not, {refinement.BuildFailureMessage(value)}";
    }
}
