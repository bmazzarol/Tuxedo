namespace Tuxedo;

/// <summary>
/// Inverts a refinement
/// </summary>
/// <typeparam name="TRefinement">refinement to invert</typeparam>
public readonly struct Not<TRefinement> : IRefinement<Not<TRefinement>>
    where TRefinement : struct, IRefinement<TRefinement>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value) => !default(TRefinement).CanBeRefined(value);

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value) =>
        $"Not: {default(TRefinement).BuildFailureMessage(value)}";
}
