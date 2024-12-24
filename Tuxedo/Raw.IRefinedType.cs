using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Tuxedo;

public static partial class Raw<T>
{
    /// <summary>
    /// Template for a refined type
    /// </summary>
    /// <typeparam name="TThis">the refined instance</typeparam>
    /// <typeparam name="TRefinement">refinement</typeparam>
    public interface IRefinedType<TThis, TRefinement>
        where TThis : IRefinedType<TThis, TRefinement>
        where TRefinement : IRefinement<TRefinement, T>
    {
        /// <summary>
        /// The underlying refined value
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Implicit conversion from the refined to the raw value
        /// </summary>
        /// <param name="refinedValue">refined value</param>
        /// <returns>underlying raw value</returns>
        public static abstract implicit operator T(TThis refinedValue);

        /// <summary>
        /// Explicit conversion from the raw to the refined value
        /// </summary>
        /// <param name="value">raw value</param>
        /// <returns>refined value</returns>
        public static abstract explicit operator TThis(T value);

        /// <summary>
        /// Refines the value or throws
        /// </summary>
        /// <remarks>
        /// This can be auto implemented through the <see cref="ParseInternal"/>
        /// </remarks>
        /// <param name="value">raw value</param>
        /// <returns>refined value</returns>
        /// <exception cref="RefinementFailureException">if the refinement fails</exception>
        static abstract TThis Parse(T value);

        /// <summary>
        /// Try and refine the raw value
        /// </summary>
        /// <remarks>
        /// This can be auto implemented through the <see cref="TryParseInternal"/>
        /// </remarks>
        /// <param name="value">raw value</param>
        /// <param name="refined">refined value</param>
        /// <param name="failureMessage">error message</param>
        /// <returns>true if refined, false otherwise</returns>
        static abstract bool TryParse(
            T value,
            out TThis refined,
            [NotNullWhen(false)] out string? failureMessage
        );

        private static readonly Func<T, TThis> Constructor = BuildConstructorFunc();

        private static Func<T, TThis> BuildConstructorFunc()
        {
            var inputType = typeof(T);
            var param = Expression.Parameter(inputType, "val");
#pragma warning disable S3011
            var ctor =
                typeof(TThis).GetConstructor(
                    BindingFlags.Instance | BindingFlags.NonPublic,
                    binder: null,
                    [inputType],
                    modifiers: null
                )
                ?? throw new InvalidOperationException(
                    $"The type {typeof(TThis).FullName} does not have a private constructor with a single argument taking a parameter of type {inputType.FullName}."
                );
#pragma warning restore S3011
            var lambda = Expression.Lambda<Func<T, TThis>>(Expression.New(ctor, param), param);
            return lambda.Compile();
        }

        /// <summary>
        /// Refines the value or throws
        /// </summary>
        /// <param name="value">raw value</param>
        /// <returns>refined value</returns>
        /// <exception cref="RefinementFailureException">if the refinement fails</exception>
        public static TThis ParseInternal(T value)
        {
            if (TRefinement.Value.CanBeRefined(value, out var failureMessage))
            {
                return Constructor.Invoke(value);
            }

            throw ExceptionDispatchInfo.SetRemoteStackTrace(
                new RefinementFailureException(value, failureMessage),
                new StackTrace(skipFrames: 3).ToString()
            );
        }

        /// <summary>
        /// Try and refine the raw value
        /// </summary>
        /// <param name="value">raw value</param>
        /// <param name="refined">refined value</param>
        /// <param name="failureMessage">error message</param>
        /// <returns>true if refined, false otherwise</returns>
        public static bool TryParseInternal(
            T value,
            out TThis refined,
            [NotNullWhen(false)] out string? failureMessage
        )
        {
            if (TRefinement.Value.CanBeRefined(value, out failureMessage))
            {
                refined = Constructor.Invoke(value);
                return true;
            }

            refined = default!;
            return false;
        }
    }
}
