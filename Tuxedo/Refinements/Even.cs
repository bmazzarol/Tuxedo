using System.Collections;

namespace Tuxedo;

/// <summary>
/// Enforces that an integer value is even
/// </summary>
public readonly struct Even : IRefinement<Even>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value) =>
        value switch
        {
            short s => s % 2 == 0,
            int i => i % 2 == 0,
            long l => l % 2 == 0,
            float f => Math.Abs(f % 2) - 0 < float.Epsilon,
            double d => Math.Abs(d % 2) - 0 < double.Epsilon,
            decimal d => d % 2 == 0,
            string text => text.Length % 2 == 0,
            ICollection collection => collection.Count % 2 == 0,
            IEnumerable enumerable => enumerable.Cast<object?>().Count() % 2 == 0,
            _ => false
        };

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value) =>
        value switch
        {
            short s => $"Value must be an even number, but found {s}",
            int i => $"Value must be an even number, but found {i}",
            long l => $"Value must be an even number, but found {l}",
            float f => $"Value must be an even number, but found {f}",
            double d => $"Value must be an even number, but found {d}",
            decimal d => $"Value must be an even number, but found {d}",
            string text => $"Value must have an even length, but found {text.Length}",
            ICollection collection
                => $"Value must have an even count, but found {collection.Count}",
            IEnumerable enumerable
                => $"Value must have an even count, but found {enumerable.Cast<object?>().Count()}",
            _ => "Value must be even"
        };
}
