namespace Tuxedo;

/// <summary>
/// Inverts a refinement
/// </summary>
/// <typeparam name="TRefinement">refinement to invert</typeparam>
/// <typeparam name="T">type of value</typeparam>
public readonly struct Not<T, TRefinement> : IRefinement<Not<T, TRefinement>, T>
    where TRefinement : struct, IRefinement<TRefinement, T>
{
    /// <inheritdoc />
    public bool CanBeRefined(T value) => !default(TRefinement).CanBeRefined(value);

    /// <inheritdoc />
    public string BuildFailureMessage(T value) =>
        $"Not: {default(TRefinement).BuildFailureMessage(value)}";
}
