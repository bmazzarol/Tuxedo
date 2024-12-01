using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Tuxedo;

/// <summary>
/// Represents a refined type, the refinement is enforced by the TRefinement type which is an implementation of <see cref="Refinement{TThis,T}"/>
/// </summary>
/// <typeparam name="TRefinement">refinement on the type</typeparam>
/// <typeparam name="T">refined type</typeparam>
public readonly record struct Refined<TRefinement, T>
    where TRefinement : Refinement<TRefinement, T>, new()
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
    public static implicit operator T(Refined<TRefinement, T> refinedValue)
    {
        return refinedValue.Value;
    }

    /// <summary>
    /// Explicitly converts the underlying value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    public static implicit operator Refined<TRefinement, T>(T value)
    {
        return Refined.Refine<TRefinement, T>(value);
    }
}

/// <summary>
/// Represents a refined type, the refinement is enforced by the TRefinement type which is an implementation of <see cref="Refinement{TThis,T}"/>
/// </summary>
/// <typeparam name="TRefinement">refinement on the type</typeparam>
/// <typeparam name="TRaw">raw refined type</typeparam>
/// <typeparam name="TRefined">refined type</typeparam>
[StructLayout(LayoutKind.Auto)]
public readonly record struct Refined<TRefinement, TRaw, TRefined>
    where TRefinement : Refinement<TRefinement, TRaw, TRefined>, new()
{
    /// <summary>
    /// The underlying value of the refined type
    /// </summary>
    public TRaw RawValue { get; }

    /// <summary>
    /// The value of the refined type
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
    public static implicit operator TRaw(Refined<TRefinement, TRaw, TRefined> refinedValue) =>
        refinedValue.RawValue;

    /// <summary>
    /// Implicitly converts the refined value to its underlying refined type
    /// </summary>
    /// <param name="refinedValue">refined value</param>
    /// <returns>underlying refined value</returns>
    public static implicit operator TRefined(Refined<TRefinement, TRaw, TRefined> refinedValue) =>
        refinedValue.RefinedValue;

    /// <summary>
    /// Explicitly converts the underlying value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    public static implicit operator Refined<TRefinement, TRaw, TRefined>(TRaw value) =>
        Refined.Refine<TRefinement, TRaw, TRefined>(value);

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
    /// <param name="failureMessage">failure message if the value cannot be refined</param>
    /// <typeparam name="TRefinement">refinement applied to the type</typeparam>
    /// <typeparam name="T">type to refine</typeparam>
    /// <returns>true if the value was refined; otherwise, false</returns>
    public static bool TryRefine<TRefinement, T>(
        T value,
        out Refined<TRefinement, T> refined,
        [NotNullWhen(false)] out string? failureMessage
    )
        where TRefinement : Refinement<TRefinement, T>, new()
    {
        if (!Refinement<TRefinement, T>.Value.CanBeRefined(value, out failureMessage))
        {
            refined = default;
            return false;
        }

        refined = new Refined<TRefinement, T>(value);
        return true;
    }

    /// <summary>
    /// Tries to refine a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <param name="refined">refined value</param>
    /// <param name="failureMessage">failure message if the value cannot be refined</param>
    /// <typeparam name="TRefinement">refinement applied to the type</typeparam>
    /// <typeparam name="TRaw">raw type to refine</typeparam>
    /// <typeparam name="TRefined">refined type</typeparam>
    /// <returns>true if the value was refined; otherwise, false</returns>
    public static bool TryRefine<TRefinement, TRaw, TRefined>(
        TRaw value,
        out Refined<TRefinement, TRaw, TRefined> refined,
        [NotNullWhen(false)] out string? failureMessage
    )
        where TRefinement : Refinement<TRefinement, TRaw, TRefined>, new()
    {
        if (
            !Refinement<TRefinement, TRaw, TRefined>.Value.CanBeRefined(
                value,
                out var refinedValue,
                out failureMessage
            )
        )
        {
            refined = default;
            return false;
        }

        refined = new Refined<TRefinement, TRaw, TRefined>(value, refinedValue);
        return true;
    }

    [DoesNotReturn]
    private static void Throw<T>(T value, string message)
    {
        var exception = new RefinementFailureException(value, message);
        ExceptionDispatchInfo.SetRemoteStackTrace(
            exception,
            new StackTrace(skipFrames: 3).ToString()
        );
        throw exception;
    }

    /// <summary>
    /// Refines a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <typeparam name="T">type to refine</typeparam>
    /// <typeparam name="TRefinement">refinement applied to the type</typeparam>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    public static Refined<TRefinement, T> Refine<TRefinement, T>(T value)
        where TRefinement : Refinement<TRefinement, T>, new()
    {
        if (Refinement<TRefinement, T>.Value.CanBeRefined(value, out var failureMessage))
        {
            return new Refined<TRefinement, T>(value);
        }

        Throw(value, failureMessage);
        return default;
    }

    /// <summary>
    /// Refines a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <typeparam name="TRefinement">refinement applied to the type</typeparam>
    /// <typeparam name="TRaw">raw type to refine</typeparam>
    /// <typeparam name="TRefined">refined type</typeparam>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    public static Refined<TRefinement, TRaw, TRefined> Refine<TRefinement, TRaw, TRefined>(
        TRaw value
    )
        where TRefinement : Refinement<TRefinement, TRaw, TRefined>, new()
    {
        if (
            Refinement<TRefinement, TRaw, TRefined>.Value.CanBeRefined(
                value,
                out var refinedValue,
                out var failureMessage
            )
        )
        {
            return new Refined<TRefinement, TRaw, TRefined>(value, refinedValue);
        }

        Throw(value, failureMessage);
        return default;
    }
}
