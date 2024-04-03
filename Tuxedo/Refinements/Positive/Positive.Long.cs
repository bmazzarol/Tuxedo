namespace Tuxedo;

/// <summary>
/// Ensures that an numeric value is positive
/// </summary>
public readonly partial struct Positive : IRefinement<Positive, long>
{
    bool IRefinement<Positive, long>.CanBeRefined(long value) => value > 0;

    string IRefinement<Positive, long>.BuildFailureMessage(long value) =>
        $"Value must be positive, but found {value}";
}
