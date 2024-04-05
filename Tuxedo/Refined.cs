using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Tuxedo.Extensions;

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

    /// <summary>
    /// Deconstructs the refined value
    /// </summary>
    /// <param name="value">underlying raw value</param>
    public void Deconstruct(out T value)
    {
        value = Value;
    }
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

    /// <summary>
    /// Deconstructs the refined value
    /// </summary>
    /// <param name="rawValue">underlying raw value</param>
    /// <param name="refinedValue">underlying refined value</param>
    public void Deconstruct(out TRaw rawValue, out TRefined refinedValue)
    {
        rawValue = RawValue;
        refinedValue = RefinedValue;
    }
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
        if (!refinement.CanBeRefined(value, out _))
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
        if (!refinement.TryRefine(value, out var refinedValue, out _))
        {
            refined = default;
            return false;
        }

        refined = new Refined<TRaw, TRefined, TRefinement>(value, refinedValue);
        return true;
    }

    [DoesNotReturn]
    private static void Throw<T>(T value, string message)
    {
        throw new RefinementFailureException(value, message).WithStackTrace(
            new StackTrace(skipFrames: 3)
        );
    }

    /// <summary>
    /// Refines a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <typeparam name="T">type to refine</typeparam>
    /// <typeparam name="TRefinement">refinement applied to the type</typeparam>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    public static Refined<T, TRefinement> Refine<T, TRefinement>(T value)
        where TRefinement : struct, IRefinement<TRefinement, T>
    {
        var refinement = default(TRefinement);
        if (refinement.CanBeRefined(value, out var failureMessage))
        {
            return new Refined<T, TRefinement>(value);
        }

        Throw(value, failureMessage);
        return default;
    }

    /// <summary>
    /// Refines a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <typeparam name="TRaw">raw type to refine</typeparam>
    /// <typeparam name="TRefined">refined type</typeparam>
    /// <typeparam name="TRefinement">refinement applied to the type</typeparam>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    public static Refined<TRaw, TRefined, TRefinement> Refine<TRaw, TRefined, TRefinement>(
        TRaw value
    )
        where TRefinement : struct, IRefinement<TRefinement, TRaw, TRefined>
    {
        var refinement = default(TRefinement);
        if (refinement.TryRefine(value, out var refinedValue, out var failureMessage))
        {
            return new Refined<TRaw, TRefined, TRefinement>(value, refinedValue);
        }

        Throw(value, failureMessage);
        return default;
    }
}
