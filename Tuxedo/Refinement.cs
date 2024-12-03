﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Tuxedo;

/// <summary>
/// Defines a contract that can be used to refine some type T.
/// A refined type is a type that is a subset of another type, limited by some predicate.
/// </summary>
/// <typeparam name="TThis">type of the refinement</typeparam>
/// <typeparam name="T">type to refine</typeparam>
public abstract class Refinement<TThis, T>
    where TThis : Refinement<TThis, T>, new()
{
    /// <summary>
    /// Singleton instance of the refinement
    /// </summary>
    internal static TThis Inst { get; } = new();

    /// <summary>
    /// Tests if the value can be refined by this instance.
    /// </summary>
    /// <param name="value">value to test for refinement</param>
    /// <param name="failureMessage">failure message returned if the value cannot be refined</param>
    /// <returns>true if the value can be refined; otherwise, false</returns>
    public virtual bool CanBeRefined(T value, [NotNullWhen(false)] out string? failureMessage)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract bool IsRefined(T value);

    /// <summary>
    /// Builds a failure message for the non-refined value
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>failure message</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract string BuildFailureMessage(T value);

    /// <summary>
    /// Refines the value to a refined type
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Refined<TThis, T> Refine(T value)
    {
        return Refined.Refine<TThis, T>(value);
    }

    /// <summary>
    /// Tries to refine a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <param name="refined">refined value</param>
    /// <param name="failureMessage">failure message if the value cannot be refined</param>
    /// <returns>true if the value was refined; otherwise, false</returns>
    public static bool TryRefine(
        T value,
        out Refined<TThis, T> refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        return Refined.TryRefine(value, out refined, out failureMessage);
    }
}

/// <summary>
/// Defines that a contract can be used to refine some type T, with a refined result.
/// </summary>
/// <typeparam name="TThis">type of the refinement</typeparam>
/// <typeparam name="TIn">input type to refine</typeparam>
/// <typeparam name="TOut">result of the refinement</typeparam>
public abstract class Refinement<TThis, TIn, TOut>
    where TThis : Refinement<TThis, TIn, TOut>, new()
{
    /// <summary>
    /// Singleton instance of the refinement
    /// </summary>
    public static TThis Inst { get; } = new();

    /// <summary>
    /// Tests if the value can be refined by this instance, returning the refined value
    /// </summary>
    /// <param name="value">value to test for refinement</param>
    /// <param name="refinedValue">refined value</param>
    /// <param name="failureMessage">failure message returned if the value cannot be refined</param>
    /// <returns>true if the value can be refined; otherwise, false</returns>
    public virtual bool CanBeRefined(
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract bool IsRefined(TIn value, [NotNullWhen(true)] out TOut refinedValue);

    /// <summary>
    /// Builds a failure message for the non-refined value
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>failure message</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract string BuildFailureMessage(TIn value);

    /// <summary>
    /// Refines the value to a refined type
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Refined<TThis, TIn, TOut> Refine(TIn value)
    {
        return Refined.Refine<TThis, TIn, TOut>(value);
    }

    /// <summary>
    /// Tries to refine a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <param name="refined">refined value</param>
    /// <param name="failureMessage">failure message if the value cannot be refined</param>
    /// <returns>true if the value was refined; otherwise, false</returns>
    public static bool TryRefine(
        TIn value,
        out Refined<TThis, TIn, TOut> refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        return Refined.TryRefine(value, out refined, out failureMessage);
    }
}
