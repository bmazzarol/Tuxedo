using System.Diagnostics.CodeAnalysis;

#pragma warning disable S2326

namespace Tuxedo;

/// <summary>
/// Defines a contract that can be used to refine some type T.
/// A refined type is a type that is a subset of another type, limited by some predicate.
/// </summary>
/// <typeparam name="TThis">the refinement instance; must be a struct type to enable lookup</typeparam>
/// <typeparam name="T">type to refine</typeparam>
public interface IRefinement<TThis, in T>
    where TThis : struct
{
    /// <summary>
    /// Tests if the value can be refined by this instance.
    /// </summary>
    /// <param name="value">value to test for refinement</param>
    /// <param name="failureMessage">failure message returned if the value cannot be refined</param>
    /// <returns>true if the value can be refined; otherwise, false</returns>
    bool CanBeRefined(T value, [NotNullWhen(false)] out string? failureMessage);
}

/// <summary>
/// Defines that a contract can be used to refine some type T, with a refined result.
/// </summary>
/// <typeparam name="TThis">the refinement instance; must be a struct type to enable lookup</typeparam>
/// <typeparam name="TIn">input type to refine</typeparam>
/// <typeparam name="TOut">result of the refinement</typeparam>
public interface IRefinement<TThis, in TIn, TOut> : IRefinement<TThis, TIn>
    where TThis : struct
{
    /// <summary>
    /// Tests if the value can be refined by this instance, returning the refined value
    /// </summary>
    /// <param name="value">value to test for refinement</param>
    /// <param name="refinedValue">refined value</param>
    /// <param name="failureMessage">failure message returned if the value cannot be refined</param>
    /// <returns>true if the value can be refined; otherwise, false</returns>
    bool TryRefine(
        TIn value,
        [NotNullWhen(true)] out TOut? refinedValue,
        [NotNullWhen(false)] out string? failureMessage
    );
}
