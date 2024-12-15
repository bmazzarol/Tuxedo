using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Tuxedo;

/// <summary>
/// Represents a refined type, the refinement is enforced by the TRefinement type which is an implementation of <see cref="IRefinement{TThis,T}"/>
/// </summary>
/// <typeparam name="TRefinement">refinement on the type</typeparam>
/// <typeparam name="T">refined type</typeparam>
public readonly record struct Refined<TRefinement, T>
    : IRefinedType<Refined<TRefinement, T>, TRefinement, T>
    where TRefinement : IRefinement<TRefinement, T>
{
    /// <inheritdoc />
    public T Value { get; }

    private Refined(T value)
    {
        Value = value;
    }

    /// <inheritdoc />
    public static implicit operator T(Refined<TRefinement, T> refinedValue)
    {
        return refinedValue.Value;
    }

    /// <inheritdoc />
    public static explicit operator Refined<TRefinement, T>(T value)
    {
        return IRefinedType<Refined<TRefinement, T>, TRefinement, T>.Refine(value);
    }

    /// <inheritdoc />
    public static Refined<TRefinement, T> CreateUnsafe(T value)
    {
        return new(value);
    }

    /// <inheritdoc />
    public static bool TryCreate(
        T value,
        out Refined<TRefinement, T> refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        return IRefinedType<Refined<TRefinement, T>, TRefinement, T>.TryRefine(
            value,
            out refined,
            out failureMessage
        );
    }
}

/// <summary>
/// Represents a refined type, the refinement is enforced by the TRefinement type which is an implementation of <see cref="IRefinement{TThis,T}"/>
/// </summary>
/// <typeparam name="TRefinement">refinement on the type</typeparam>
/// <typeparam name="TRaw">raw refined type</typeparam>
/// <typeparam name="TRefined">refined type</typeparam>
[StructLayout(LayoutKind.Auto)]
public readonly record struct Refined<TRefinement, TRaw, TRefined>
    : IRefinedType<Refined<TRefinement, TRaw, TRefined>, TRefinement, TRaw, TRefined>
    where TRefinement : IRefinement<TRefinement, TRaw, TRefined>
{
    /// <inheritdoc />
    public TRaw RawValue { get; }

    /// <inheritdoc />
    public static implicit operator TRaw(Refined<TRefinement, TRaw, TRefined> refinedValue)
    {
        return refinedValue.RawValue;
    }

    /// <inheritdoc />
    public TRefined RefinedValue { get; }

    /// <inheritdoc />
    public static implicit operator TRefined(Refined<TRefinement, TRaw, TRefined> refinedValue)
    {
        return refinedValue.RefinedValue;
    }

    /// <inheritdoc />
    public static explicit operator Refined<TRefinement, TRaw, TRefined>(TRaw value)
    {
        return IRefinedType<
            Refined<TRefinement, TRaw, TRefined>,
            TRefinement,
            TRaw,
            TRefined
        >.Refine(value);
    }

    private Refined(TRaw rawValue, TRefined refinedValue)
    {
        RawValue = rawValue;
        RefinedValue = refinedValue;
    }

    /// <inheritdoc />
    public static Refined<TRefinement, TRaw, TRefined> CreateUnsafe(
        TRaw value,
        TRefined refinedValue
    )
    {
        return new(value, refinedValue);
    }

    /// <inheritdoc />
    public static bool TryCreate(
        TRaw value,
        out Refined<TRefinement, TRaw, TRefined> refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        return IRefinedType<
            Refined<TRefinement, TRaw, TRefined>,
            TRefinement,
            TRaw,
            TRefined
        >.TryRefine(value, out refined, out failureMessage);
    }

    /// <inheritdoc />
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
        where TRefinement : IRefinement<TRefinement, T>
    {
        return IRefinedType<Refined<TRefinement, T>, TRefinement, T>.TryRefine(
            value,
            out refined,
            out failureMessage
        );
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
        where TRefinement : IRefinement<TRefinement, TRaw, TRefined>
    {
        return IRefinedType<
            Refined<TRefinement, TRaw, TRefined>,
            TRefinement,
            TRaw,
            TRefined
        >.TryRefine(value, out refined, out failureMessage);
    }

    /// <summary>
    /// Refines a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <typeparam name="TRefinement">refinement applied to the type</typeparam>
    /// <typeparam name="T">type to refine</typeparam>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    public static Refined<TRefinement, T> Refine<TRefinement, T>(T value)
        where TRefinement : IRefinement<TRefinement, T>
    {
        return IRefinedType<Refined<TRefinement, T>, TRefinement, T>.Refine(value);
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
        where TRefinement : IRefinement<TRefinement, TRaw, TRefined>
    {
        return IRefinedType<
            Refined<TRefinement, TRaw, TRefined>,
            TRefinement,
            TRaw,
            TRefined
        >.Refine(value);
    }
}
