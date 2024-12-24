using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Defines a contract that can be used to refine some type T.
/// A refined type is a type that is a subset of another type, limited by some predicate.
/// </summary>
/// <typeparam name="TThis">type of the refinement</typeparam>
/// <typeparam name="T">type to refine</typeparam>
public interface IRefinement<out TThis, in T>
    where TThis : IRefinement<TThis, T>
{
    /// <summary>
    /// Singleton instance of the refinement
    /// </summary>
    public static abstract TThis Value { get; }

    /// <summary>
    /// Tests if the value can be refined by this instance.
    /// </summary>
    /// <param name="value">value to test for refinement</param>
    /// <param name="failureMessage">failure message returned if the value cannot be refined</param>
    /// <returns>true if the value can be refined; otherwise, false</returns>
    bool CanBeRefined(T value, [NotNullWhen(false)] out string? failureMessage)
    {
        if (IsRefined(value))
        {
            failureMessage = null;
            return true;
        }

        failureMessage = BuildFailureMessage(value);
        return false;
    }

    /// <summary>
    /// Returns true if the value is refined
    /// </summary>
    /// <param name="value">value to test</param>
    /// <returns>true if the value is refined; otherwise, false</returns>
    bool IsRefined(T value);

    /// <summary>
    /// Builds a failure message for the non-refined value
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>failure message</returns>
    string BuildFailureMessage(T value);
}

/// <summary>
/// Defines that a contract can be used to refine some type T, with a refined result.
/// </summary>
/// <typeparam name="TThis">type of the refinement</typeparam>
/// <typeparam name="TIn">input type to refine</typeparam>
/// <typeparam name="TOut">result of the refinement</typeparam>
public interface IRefinement<out TThis, in TIn, TOut>
    where TThis : IRefinement<TThis, TIn, TOut>
{
    /// <summary>
    /// Singleton instance of the refinement
    /// </summary>
    static abstract TThis Value { get; }

    /// <summary>
    /// Tests if the value can be refined by this instance, returning the refined value
    /// </summary>
    /// <param name="value">value to test for refinement</param>
    /// <param name="refinedValue">refined value</param>
    /// <param name="failureMessage">failure message returned if the value cannot be refined</param>
    /// <returns>true if the value can be refined; otherwise, false</returns>
    bool CanBeRefined(
        TIn value,
        [NotNullWhen(true)] out TOut? refinedValue,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (IsRefined(value, out refinedValue))
        {
            failureMessage = null;
            return true;
        }

        failureMessage = BuildFailureMessage(value);
        return false;
    }

    /// <summary>
    /// Returns true if the value is refined, and returns the refined value
    /// </summary>
    /// <param name="value">value to test</param>
    /// <param name="refinedValue">refined value</param>
    /// <returns>true if the value is refined; otherwise, false</returns>
    bool IsRefined(TIn value, [NotNullWhen(true)] out TOut refinedValue);

    /// <summary>
    /// Builds a failure message for the non-refined value
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>failure message</returns>
    string BuildFailureMessage(TIn value);
}
