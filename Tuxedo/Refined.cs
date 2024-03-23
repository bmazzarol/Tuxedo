﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Tuxedo;

/// <summary>
/// Represents a refined type, the refinement is enforced by the TRefinement type which is an implementation of <see cref="IRefinement{TThis,T}"/>
/// </summary>
/// <typeparam name="T">refined type</typeparam>
/// <typeparam name="TRefinement">refinement on the type</typeparam>
public readonly record struct Refined<T, TRefinement>
    where TRefinement : struct, IRefinement<TRefinement, T>
{
    /// <summary>
    /// The underlying value of the refined type
    /// </summary>
    public T Value { get; }

    internal Refined(T value)
    {
        Value = value;
    }

    /// <summary>
    /// Implicitly converts the refined value to its underlying type
    /// </summary>
    /// <param name="refinedValue">refined value</param>
    /// <returns>underlying value</returns>
    public static implicit operator T(Refined<T, TRefinement> refinedValue) => refinedValue.Value;

    /// <summary>
    /// Explicitly converts the underlying value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    public static implicit operator Refined<T, TRefinement>(T value) =>
        Refined.Refine<T, TRefinement>(value);
}

/// <summary>
/// Represents a refined type, the refinement is enforced by the TRefinement type which is an implementation of <see cref="IRefinement{TThis,T}"/>
/// </summary>
/// <typeparam name="TRaw">raw refined type</typeparam>
/// <typeparam name="TRefined">refined type</typeparam>
/// <typeparam name="TRefinement">refinement on the type</typeparam>
[StructLayout(LayoutKind.Auto)]
public readonly record struct Refined<TRaw, TRefined, TRefinement>
    where TRefinement : struct, IRefinement<TRefinement, TRaw, TRefined>
{
    /// <summary>
    /// The underlying value of the refined type
    /// </summary>
    public TRaw RawValue { get; }

    /// <summary>
    /// The  value of the refined type
    /// </summary>
    public TRefined RefinedValue { get; }

    internal Refined(TRaw value, TRefined refinedValue)
    {
        RawValue = value;
        RefinedValue = refinedValue;
    }

    /// <summary>
    /// Implicitly converts the refined value to its underlying raw type
    /// </summary>
    /// <param name="refinedValue">refined value</param>
    /// <returns>underlying raw value</returns>
    public static implicit operator TRaw(Refined<TRaw, TRefined, TRefinement> refinedValue) =>
        refinedValue.RawValue;

    /// <summary>
    /// Implicitly converts the refined value to its underlying refined type
    /// </summary>
    /// <param name="refinedValue">refined value</param>
    /// <returns>underlying refined value</returns>
    public static implicit operator TRefined(Refined<TRaw, TRefined, TRefinement> refinedValue) =>
        refinedValue.RefinedValue;

    /// <summary>
    /// Explicitly converts the underlying value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    public static implicit operator Refined<TRaw, TRefined, TRefinement>(TRaw value) =>
        Refined.Refine<TRaw, TRefined, TRefinement>(value);
}

/// <summary>
/// Static companion class for creating refined types
/// </summary>
public static class Refined
{
    /// <summary>
    /// Tries to refine a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <param name="refined">refined value</param>
    /// <typeparam name="T">type to refine</typeparam>
    /// <typeparam name="TRefinement">refinement applied to the type</typeparam>
    /// <returns>true if the value was refined; otherwise, false</returns>
    public static bool TryRefine<T, TRefinement>(T value, out Refined<T, TRefinement> refined)
        where TRefinement : struct, IRefinement<TRefinement, T>
    {
        var refinement = default(TRefinement);
        if (!refinement.CanBeRefined(value))
        {
            refined = default;
            return false;
        }

        refined = new Refined<T, TRefinement>(value);
        return true;
    }

    /// <summary>
    /// Tries to refine a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <param name="refined">refined value</param>
    /// <typeparam name="TRaw">raw type to refine</typeparam>
    /// <typeparam name="TRefined">refined type</typeparam>
    /// <typeparam name="TRefinement">refinement applied to the type</typeparam>
    /// <returns>true if the value was refined; otherwise, false</returns>
    public static bool TryRefine<TRaw, TRefined, TRefinement>(
        TRaw value,
        out Refined<TRaw, TRefined, TRefinement> refined
    )
        where TRefinement : struct, IRefinement<TRefinement, TRaw, TRefined>
    {
        var refinement = default(TRefinement);
        if (!refinement.TryRefine(value, out var refinedValue))
        {
            refined = default;
            return false;
        }

        refined = new Refined<TRaw, TRefined, TRefinement>(value, refinedValue);
        return true;
    }

    [DoesNotReturn]
    [SuppressMessage("Design", "MA0026:Fix TODO comment")]
    [SuppressMessage("Info Code Smell", "S1135:Track uses of \"TODO\" tags")]
    private static void Throw<T, TRefinement>(T value, TRefinement refinement)
        where TRefinement : struct, IRefinement<TRefinement, T>
    {
        // TODO: find a way to rewind the stack trace to the caller
        throw new RefinementFailureException(value, refinement.BuildFailureMessage(value));
    }

    /// <summary>
    /// Refines a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <param name="tryApplyRefinement">flag to indicate if the refinement should be applied if the value is not already refined</param>
    /// <typeparam name="T">type to refine</typeparam>
    /// <typeparam name="TRefinement">refinement applied to the type</typeparam>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    public static Refined<T, TRefinement> Refine<T, TRefinement>(
        T value,
        bool tryApplyRefinement = true
    )
        where TRefinement : struct, IRefinement<TRefinement, T>
    {
        var refinement = default(TRefinement);
        if (refinement.CanBeRefined(value))
        {
            return new Refined<T, TRefinement>(value);
        }

        if (tryApplyRefinement && refinement.TryApplyRefinement(value, out var newValue))
        {
            return new Refined<T, TRefinement>(newValue);
        }

        Throw(value, refinement);
        return default;
    }

    /// <summary>
    /// Refines a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <param name="tryApplyRefinement">flag to indicate if the refinement should be applied if the value is not already refined</param>
    /// <typeparam name="TRaw">raw type to refine</typeparam>
    /// <typeparam name="TRefined">refined type</typeparam>
    /// <typeparam name="TRefinement">refinement applied to the type</typeparam>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    public static Refined<TRaw, TRefined, TRefinement> Refine<TRaw, TRefined, TRefinement>(
        TRaw value,
        bool tryApplyRefinement = true
    )
        where TRefinement : struct, IRefinement<TRefinement, TRaw, TRefined>
    {
        var refinement = default(TRefinement);
        if (refinement.TryRefine(value, out var refinedValue))
        {
            return new Refined<TRaw, TRefined, TRefinement>(value, refinedValue);
        }
        if (
            tryApplyRefinement
            && refinement.TryApplyRefinement(value, out var newValue)
            && refinement.TryRefine(newValue, out refinedValue)
        )
        {
            return new Refined<TRaw, TRefined, TRefinement>(value, refinedValue);
        }

        Throw(value, refinement);
        return default;
    }
}