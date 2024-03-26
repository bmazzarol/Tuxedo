namespace Tuxedo;

/// <summary>
/// Ensures that an integer value is positive
/// </summary>
public readonly struct Positive : IRefinement<Positive>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value) =>
        value switch
        {
            short s => s > 0,
            int i => i > 0,
            long l => l > 0,
            float f => f > 0,
            double d => d > 0,
            decimal d => d > 0,
            _ => false
        };

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value) =>
        value switch
        {
            short s => $"Value must be positive, but found {s}",
            int i => $"Value must be positive, but found {i}",
            long l => $"Value must be positive, but found {l}",
            float f => $"Value must be positive, but found {f}",
            double d => $"Value must be positive, but found {d}",
            decimal d => $"Value must be positive, but found {d}",
            _ => "Value must be positive"
        };
}
