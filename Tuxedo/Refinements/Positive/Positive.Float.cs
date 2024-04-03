namespace Tuxedo;

/// <summary>
/// Ensures that an numeric value is positive
/// </summary>
public readonly partial struct Positive : IRefinement<Positive, float>
{
    bool IRefinement<Positive, float>.CanBeRefined(float value) => value > 0;

    string IRefinement<Positive, float>.BuildFailureMessage(float value) =>
        $"Value must be positive, but found {value}";
}
