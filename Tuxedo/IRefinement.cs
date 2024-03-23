using System.Diagnostics.CodeAnalysis;

#pragma warning disable S2326

namespace Tuxedo;

/// <summary>
/// Defines a contract that can be used to refine some type T.
/// A refined type is a type that is a subset of another type, limited by some predicate.
/// </summary>
/// <typeparam name="TThis">the refinement instance; must be a struct type to enable lookup</typeparam>
/// <typeparam name="T">some type to try and refine</typeparam>
public interface IRefinement<TThis, T>
    where TThis : struct
{
    /// <summary>
    /// Tests if the value can be refined by this instance.
    /// </summary>
    /// <param name="value">value to test for refinement</param>
    /// <returns>true if the value can be refined; otherwise, false</returns>
    bool CanBeRefined(T value);

    /// <summary>
    /// Attempts to refine the given value.
    /// Can be used to refine a value to match the refinement requirements, instead of throwing an exception.
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <param name="refinedValue">refined value</param>
    /// <returns>true if the value was refined; otherwise, false</returns>
    bool TryApplyRefinement(T value, [NotNullWhen(true)] out T? refinedValue);

    /// <summary>
    /// Builds a failure message for the given value when it cannot be refined.
    /// </summary>
    /// <param name="value">value that cannot be refined</param>
    /// <returns>failure message</returns>
    string BuildFailureMessage(T value);
}

/// <summary>
/// Defines a contract that can be used to refine some type T.
/// A refined type is a type that is a subset of another type, limited by some predicate.
/// </summary>
/// <typeparam name="TThis">the refinement instance; must be a struct type to enable lookup</typeparam>
/// <typeparam name="TIn">some type to try and refine</typeparam>
/// <typeparam name="TOut">the refined type</typeparam>
public interface IRefinement<TThis, TIn, TOut> : IRefinement<TThis, TIn>
    where TThis : struct
{
    /// <summary>
    /// Tests if the value can be refined by this instance.
    /// </summary>
    /// <param name="value">value to test for refinement</param>
    /// <param name="refinedValue">refined value</param>
    /// <returns>true if the value can be refined; otherwise, false</returns>
    bool TryRefine(TIn value, [NotNullWhen(true)] out TOut? refinedValue);
}
