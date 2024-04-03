namespace Tuxedo;

/// <summary>
/// Ensures that an numeric value is positive
/// </summary>
public readonly partial struct Positive : IRefinement<Positive, decimal>
{
    bool IRefinement<Positive, decimal>.CanBeRefined(decimal value) => value > 0;

    string IRefinement<Positive, decimal>.BuildFailureMessage(decimal value) =>
        $"Value must be positive, but found {value}";
}
