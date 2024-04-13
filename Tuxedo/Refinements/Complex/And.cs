using System.Diagnostics.CodeAnalysis;

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
    public bool CanBeRefined(T value, [NotNullWhen(false)] out string? failureMessage)
    {
        switch (
            (
                default(TFirstRefinement).CanBeRefined(value, out var firstFailureMessage),
                default(TSecondRefinement).CanBeRefined(value, out var secondFailureMessage)
            )
        )
        {
            case (true, true):
                failureMessage = null;
                return true;
            case (false, false):
                failureMessage = $"{firstFailureMessage} and {secondFailureMessage}";
                return false;
            case (true, false):
                failureMessage = secondFailureMessage ?? string.Empty;
                return false;
            case (false, true):
                failureMessage = firstFailureMessage ?? string.Empty;
                return false;
        }
    }
}
