using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Inverts a refinement
/// </summary>
/// <typeparam name="T">type of value</typeparam>
/// <typeparam name="TRefinement">refinement to invert</typeparam>
public readonly struct Not<T, TRefinement> : IRefinement<Not<T, TRefinement>, T>
    where TRefinement : struct, IRefinement<TRefinement, T>
{
    /// <inheritdoc />
    public bool CanBeRefined(T value, [NotNullWhen(false)] out string? failureMessage)
    {
        if (!default(TRefinement).CanBeRefined(value, out _))
        {
            failureMessage = null;
            return true;
        }

        failureMessage =
            $"Refinement '{default(TRefinement).GetType().Name}' passed when it should have failed";
        return false;
    }
}
