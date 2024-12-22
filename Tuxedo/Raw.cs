using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.ExceptionServices.ExceptionDispatchInfo;

namespace Tuxedo;

/// <summary>
/// Represents an unrefined raw value
/// </summary>
/// <typeparam name="T">some unrefined type</typeparam>
public readonly struct Raw<T>
{
    /// <summary>
    /// Refines the raw value with the given refinement
    /// </summary>
    /// <typeparam name="TRefinement">refinement</typeparam>
    public readonly struct Refined<TRefinement>
        where TRefinement : IRefinement<TRefinement, T>
    {
        /// <summary>
        /// Underlying value
        /// </summary>
        public T Value { get; }

        private Refined(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Implicit conversion from the refined to the raw value
        /// </summary>
        /// <param name="this">refined value</param>
        /// <returns>underlying raw value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator T(Refined<TRefinement> @this) => @this.Value;

        /// <summary>
        /// Explicit conversion from the raw to the refined value
        /// </summary>
        /// <param name="value">raw value</param>
        /// <returns>refined value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Refined<TRefinement>(T value)
        {
            return Parse(value);
        }

        /// <summary>
        /// Try and refine the raw value
        /// </summary>
        /// <param name="value">raw value</param>
        /// <param name="refined">refined value</param>
        /// <param name="failureMessage">error message</param>
        /// <returns>true if refined, false otherwise</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryParse(
            T value,
            out Refined<TRefinement> refined,
            [NotNullWhen(false)] out string? failureMessage
        )
        {
            if (TRefinement.Value.CanBeRefined(value, out failureMessage))
            {
                refined = new Refined<TRefinement>(value);
                return true;
            }

            refined = default;
            return false;
        }

        /// <summary>
        /// Refines the value or throws
        /// </summary>
        /// <param name="value">raw value</param>
        /// <returns>refined value</returns>
        /// <exception cref="RefinementFailureException">if the refinement fails</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Refined<TRefinement> Parse(T value)
        {
            if (TRefinement.Value.CanBeRefined(value, out var failureMessage))
            {
                return new(value);
            }

            throw SetRemoteStackTrace(
                new RefinementFailureException(value, failureMessage),
                new StackTrace(skipFrames: 2).ToString()
            );
        }
    }

    /// <summary>
    /// Alternate result value produced from refinement of the raw value
    /// </summary>
    /// <typeparam name="TAlternate">alternative type</typeparam>
    public readonly struct Produce<TAlternate>
    {
        /// <summary>
        /// Refines the raw value with the given refinement
        /// </summary>
        /// <typeparam name="TRefinement">refinement</typeparam>
#pragma warning disable S3218
        [StructLayout(LayoutKind.Auto)]
        public readonly struct Refined<TRefinement>
#pragma warning restore S3218
            where TRefinement : IRefinement<TRefinement, T, TAlternate>
        {
            /// <summary>
            /// Underlying raw value
            /// </summary>
            public T RawValue { get; }

            /// <summary>
            /// The alternate refined version of the underlying value
            /// </summary>
            public TAlternate RefinedValue { get; }

            private Refined(T raw, TAlternate refinedValue)
            {
                RawValue = raw;
                RefinedValue = refinedValue;
            }

            /// <summary>
            /// Implicit conversion from the refined to the raw value
            /// </summary>
            /// <param name="this">refined value</param>
            /// <returns>underlying raw value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator T(Refined<TRefinement> @this) => @this.RawValue;

            /// <summary>
            /// Implicit conversion from the refined to the refined value
            /// </summary>
            /// <param name="this">refined value</param>
            /// <returns>refined value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator TAlternate(Refined<TRefinement> @this) =>
                @this.RefinedValue;

            /// <summary>
            /// Explicit conversion from the raw to the refined type
            /// </summary>
            /// <param name="value">raw type</param>
            /// <returns>refined typ</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static explicit operator Refined<TRefinement>(T value)
            {
                return Parse(value);
            }

            /// <summary>
            /// Try and refine the raw value
            /// </summary>
            /// <param name="value">raw value</param>
            /// <param name="refined">refined value</param>
            /// <param name="failureMessage">error message</param>
            /// <returns>true if the refined, false otherwise</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool TryParse(
                T value,
                out Refined<TRefinement> refined,
                [NotNullWhen(false)] out string? failureMessage
            )
            {
                if (TRefinement.Value.CanBeRefined(value, out var refinedValue, out failureMessage))
                {
                    refined = new Refined<TRefinement>(value, refinedValue);
                    return true;
                }

                refined = default;
                return false;
            }

            /// <summary>
            /// Refines the value or throws
            /// </summary>
            /// <param name="value">raw value</param>
            /// <returns>refined value</returns>
            /// <exception cref="RefinementFailureException">if the refinement fails</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Refined<TRefinement> Parse(T value)
            {
                if (
                    TRefinement.Value.CanBeRefined(
                        value,
                        out var refinedValue,
                        out var failureMessage
                    )
                )
                {
                    return new(value, refinedValue);
                }

                throw SetRemoteStackTrace(
                    new RefinementFailureException(value, failureMessage),
                    new StackTrace(skipFrames: 2).ToString()
                );
            }
        }
    }
}
