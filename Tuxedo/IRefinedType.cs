using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.ExceptionServices.ExceptionDispatchInfo;

namespace Tuxedo;

/// <summary>
/// Represents a refined type, the refinement is enforced by the TRefinement type which is an implementation of <see cref="IRefinement{TThis,T}"/>
/// </summary>
/// <typeparam name="TThis">refined type</typeparam>
/// <typeparam name="TRefinement">refinement on the type</typeparam>
/// <typeparam name="T">refined type</typeparam>
public interface IRefinedType<TThis, TRefinement, T>
    where TThis : IRefinedType<TThis, TRefinement, T>
    where TRefinement : IRefinement<TRefinement, T>
{
    /// <summary>
    /// The underlying value of the refined type
    /// </summary>
    T Value { get; }

    /// <summary>
    /// Implicitly converts the refined value to its underlying type
    /// </summary>
    /// <param name="refinedValue">refined value</param>
    /// <returns>underlying value</returns>
    static abstract implicit operator T(TThis refinedValue);

    /// <summary>
    /// Explicitly converts the underlying value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    static abstract implicit operator TThis(T value);

    /// <summary>
    /// Creates the refined type without checking if the value can be refined, this should only be used when the value has already been checked.
    /// It is intended to be used by the implicit operator and so should not be used directly.
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>potentially refined value</returns>
    static abstract TThis CreateUnsafe(T value);

    /// <summary>
    /// Tries to refine a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <param name="refined">refined value</param>
    /// <param name="failureMessage">failure message if the value cannot be refined</param>
    /// <returns>true if the value was refined; otherwise, false</returns>
    static bool TryRefine(
        T value,
        [NotNullWhen(true)] out TThis? refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (!TRefinement.Value.CanBeRefined(value, out failureMessage))
        {
            refined = default;
            return false;
        }

        refined = TThis.CreateUnsafe(value);
        return true;
    }

    /// <summary>
    /// Refines a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    static TThis Refine(T value)
    {
        if (TRefinement.Value.CanBeRefined(value, out var failureMessage))
        {
            return TThis.CreateUnsafe(value);
        }

        throw SetRemoteStackTrace(
            new RefinementFailureException(value, failureMessage),
            new StackTrace(skipFrames: 2).ToString()
        );
    }
}

/// <summary>
/// Represents a refined type, the refinement is enforced by the TRefinement type which is an implementation of <see cref="IRefinement{TThis,TRaw,TRefined}"/>.
/// This interface is used when a refined type is the result of determining if a value can be refined.
/// This provides benefits to performance as the refined values is produced as a result of the check.
/// </summary>
/// <typeparam name="TThis">refined type</typeparam>
/// <typeparam name="TRefinement">refinement on the type</typeparam>
/// <typeparam name="TRaw">raw type to refine</typeparam>
/// <typeparam name="TRefined">refined type</typeparam>
public interface IRefinedType<TThis, TRefinement, TRaw, TRefined>
    where TThis : IRefinedType<TThis, TRefinement, TRaw, TRefined>
    where TRefinement : IRefinement<TRefinement, TRaw, TRefined>
{
    /// <summary>
    /// The underlying value of the refined type
    /// </summary>
    TRaw RawValue { get; }

    /// <summary>
    /// Implicitly converts the refined value to its underlying raw type
    /// </summary>
    /// <param name="refinedValue">refined value</param>
    /// <returns>underlying raw value</returns>
    static abstract implicit operator TRaw(TThis refinedValue);

    /// <summary>
    /// The refined value
    /// </summary>
    TRefined RefinedValue { get; }

    /// <summary>
    /// Implicitly converts the refined value to its underlying refined type
    /// </summary>
    /// <param name="refinedValue">refined value</param>
    /// <returns>underlying value</returns>
    static abstract implicit operator TRefined(TThis refinedValue);

    /// <summary>
    /// Explicitly converts the underlying value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    static abstract implicit operator TThis(TRaw value);

    /// <summary>
    /// Creates the refined type without checking if the value can be refined, this should only be used when the value has already been checked.
    /// It is intended to be used by the implicit operator and so should not be used directly.
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <param name="refinedValue">refined value</param>
    /// <returns>potentially refined value</returns>
    static abstract TThis CreateUnsafe(TRaw value, TRefined refinedValue);

    /// <summary>
    /// Tries to refine a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <param name="refined">refined value</param>
    /// <param name="failureMessage">failure message if the value cannot be refined</param>
    /// <returns>true if the value was refined; otherwise, false</returns>
    static bool TryRefine(
        TRaw value,
        [NotNullWhen(true)] out TThis? refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (!TRefinement.Value.CanBeRefined(value, out var refinedValue, out failureMessage))
        {
            refined = default;
            return false;
        }

        refined = TThis.CreateUnsafe(value, refinedValue);
        return true;
    }

    /// <summary>
    /// Refines a value to a refined type
    /// </summary>
    /// <param name="value">value to refine</param>
    /// <returns>refined value</returns>
    /// <exception cref="RefinementFailureException">thrown if the value cannot be refined</exception>
    public static TThis Refine(TRaw value)
    {
        if (TRefinement.Value.CanBeRefined(value, out var refinedValue, out var failureMessage))
        {
            return TThis.CreateUnsafe(value, refinedValue);
        }

        throw SetRemoteStackTrace(
            new RefinementFailureException(value, failureMessage),
            new StackTrace(skipFrames: 2).ToString()
        );
    }

    /// <summary>
    /// Deconstructs the refined type into its raw and refined values
    /// </summary>
    /// <param name="rawValue">raw value</param>
    /// <param name="refinedValue">refined value</param>
    void Deconstruct(out TRaw rawValue, out TRefined refinedValue);
}
