using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Tuxedo;

public static partial class Raw<T>
{
    public static partial class Produces<TAlternate>
    {
        /// <summary>
        /// Template for a refined type
        /// </summary>
        /// <typeparam name="TThis">the refined instance</typeparam>
        /// <typeparam name="TRefinement">refinement</typeparam>
#pragma warning disable S3218
        public interface IRefinedType<TThis, TRefinement>
#pragma warning restore S3218
            where TThis : IRefinedType<TThis, TRefinement>
            where TRefinement : IRefinement<TRefinement, T, TAlternate>
        {
            /// <summary>
            /// The underlying refined value
            /// </summary>
            T RawValue { get; }

            /// <summary>
            /// The refined value
            /// </summary>
            TAlternate RefinedValue { get; }

            /// <summary>
            /// Implicit conversion from the refined to the raw value
            /// </summary>
            /// <param name="refinedValue">refined value</param>
            /// <returns>underlying raw value</returns>
            static abstract implicit operator T(TThis refinedValue);

            /// <summary>
            /// Implicit conversion from the refined to the refined value
            /// </summary>
            /// <param name="refinedValue">refined value</param>
            /// <returns>refined value</returns>
            static abstract implicit operator TAlternate(TThis refinedValue);

            /// <summary>
            /// Explicit conversion from the raw to the refined type
            /// </summary>
            /// <param name="value">raw type</param>
            /// <returns>refined typ</returns>
            static abstract explicit operator TThis(T value);

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

            private static readonly Func<T, TAlternate, TThis> Constructor = BuildConstructorFunc();

            private static Func<T, TAlternate, TThis> BuildConstructorFunc()
            {
                var rawInputType = typeof(T);
                var refinedInputType = typeof(TAlternate);
                var thisType = typeof(TThis);
                var rawParam = Expression.Parameter(rawInputType, "rawValue");
                var refinedParam = Expression.Parameter(refinedInputType, "refinedValue");
#pragma warning disable S3011
                var ctor =
                    thisType.GetConstructor(
                        BindingFlags.Instance | BindingFlags.NonPublic,
                        binder: null,
                        [rawInputType, refinedInputType],
                        modifiers: null
                    )
                    ?? throw new InvalidOperationException(
                        $"The type {thisType.FullName} does not have a private constructor with a arguments taking parameters of type {rawInputType.FullName} and type {refinedInputType.FullName}."
                    );
#pragma warning restore S3011
                var lambda = Expression.Lambda<Func<T, TAlternate, TThis>>(
                    Expression.New(ctor, rawParam, refinedParam),
                    rawParam,
                    refinedParam
                );
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
                if (
                    TRefinement.Value.CanBeRefined(
                        value,
                        out var refinedValue,
                        out var failureMessage
                    )
                )
                {
                    return Constructor.Invoke(value, refinedValue);
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
            /// <returns>true if the refined, false otherwise</returns>
            public static bool TryParseInternal(
                T value,
                out TThis refined,
                [NotNullWhen(false)] out string? failureMessage
            )
            {
                if (TRefinement.Value.CanBeRefined(value, out var refinedValue, out failureMessage))
                {
                    refined = Constructor.Invoke(value, refinedValue);
                    return true;
                }

                refined = default!;
                return false;
            }
        }
    }
}
