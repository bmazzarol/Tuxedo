using System.Diagnostics.CodeAnalysis;

#pragma warning disable S2326

namespace Tuxedo;

/// <summary>
/// Defines a contract that can be used to refine some type T.
/// A refined type is a type that is a subset of another type, limited by some predicate.
/// </summary>
/// <typeparam name="TThis">the refinement instance; must be a struct type to enable lookup</typeparam>
public interface IRefinement<TThis>
    where TThis : struct
{
    /// <summary>
    /// Tests if the value can be refined by this instance.
    /// </summary>
    /// <param name="value">value to test for refinement</param>
    /// <returns>true if the value can be refined; otherwise, false</returns>
    bool CanBeRefined<T>(T value);

    /// <summary>
    /// Builds a failure message for the given value when it cannot be refined.
    /// </summary>
    /// <param name="value">value that cannot be refined</param>
    /// <returns>failure message</returns>
    string BuildFailureMessage<T>(T value);
}

/// <summary>
/// Defines that a contract can be used to refine some type T, with a refined result.
/// </summary>
/// <typeparam name="TThis">the refinement instance; must be a struct type to enable lookup</typeparam>
/// <typeparam name="TOut">result of the refinement</typeparam>
public interface IRefinementResult<TThis, TOut> : IRefinement<TThis>
    where TThis : struct
{
    /// <summary>
    /// Tests if the value can be refined by this instance, returning the refined value
    /// </summary>
    /// <param name="value">value to test for refinement</param>
    /// <param name="refinedValue">refined value</param>
    /// <returns>true if the value can be refined; otherwise, false</returns>
    bool TryRefine<TIn>(TIn value, [NotNullWhen(true)] out TOut? refinedValue);
}
