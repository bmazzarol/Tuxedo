namespace Tuxedo;

/// <summary>
/// Ensures that an numeric value is positive
/// </summary>
public readonly partial struct Positive : IRefinement<Positive, double>
{
    bool IRefinement<Positive, double>.CanBeRefined(double value) => value > 0;

    string IRefinement<Positive, double>.BuildFailureMessage(double value) =>
        $"Value must be positive, but found {value}";
}
