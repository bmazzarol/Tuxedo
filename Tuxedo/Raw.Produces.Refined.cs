using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Tuxedo;

public static partial class Raw<T>
{
    public static partial class Produces<TAlternate>
    {
        /// <summary>
        /// Refines the raw value with the given refinement
        /// </summary>
        /// <typeparam name="TRefinement">refinement</typeparam>
#pragma warning disable S3218
        [StructLayout(LayoutKind.Auto)]
        public readonly struct Refined<TRefinement>
            : IRefinedType<Refined<TRefinement>, TRefinement>
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

            [SuppressMessage(
                "Major Code Smell",
                "S1144:Unused private types or members should be removed"
            )]
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
            public static implicit operator T(Refined<TRefinement> @this)
            {
                return @this.RawValue;
            }

            /// <summary>
            /// Implicit conversion from the refined to the refined value
            /// </summary>
            /// <param name="this">refined value</param>
            /// <returns>refined value</returns>
            public static implicit operator TAlternate(Refined<TRefinement> @this)
            {
                return @this.RefinedValue;
            }

            /// <summary>
            /// Explicit conversion from the raw to the refined type
            /// </summary>
            /// <param name="value">raw type</param>
            /// <returns>refined typ</returns>
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
            public static bool TryParse(
                T value,
                out Refined<TRefinement> refined,
                [NotNullWhen(false)] out string? failureMessage
            )
            {
                return IRefinedType<Refined<TRefinement>, TRefinement>.TryParseInternal(
                    value,
                    out refined,
                    out failureMessage
                );
            }

            /// <summary>
            /// Refines the value or throws
            /// </summary>
            /// <param name="value">raw value</param>
            /// <returns>refined value</returns>
            /// <exception cref="RefinementFailureException">if the refinement fails</exception>
            public static Refined<TRefinement> Parse(T value)
            {
                return IRefinedType<Refined<TRefinement>, TRefinement>.ParseInternal(value);
            }
        }
    }
}
