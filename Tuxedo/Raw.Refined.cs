using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

public static partial class Raw<T>
{
    /// <summary>
    /// Refines the raw value with the given refinement
    /// </summary>
    /// <typeparam name="TRefinement">refinement</typeparam>
    public readonly struct Refined<TRefinement> : IRefinedType<Refined<TRefinement>, TRefinement>
        where TRefinement : IRefinement<TRefinement, T>
    {
        /// <summary>
        /// Underlying value
        /// </summary>
        public T Value { get; }

        [SuppressMessage(
            "Major Code Smell",
            "S1144:Unused private types or members should be removed"
        )]
        private Refined(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Implicit conversion from the refined to the raw value
        /// </summary>
        /// <param name="this">refined value</param>
        /// <returns>underlying raw value</returns>
        public static implicit operator T(Refined<TRefinement> @this)
        {
            return @this.Value;
        }

        /// <summary>
        /// Explicit conversion from the raw to the refined value
        /// </summary>
        /// <param name="value">raw value</param>
        /// <returns>refined value</returns>
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
