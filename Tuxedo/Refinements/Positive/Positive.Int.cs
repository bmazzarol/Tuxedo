namespace Tuxedo;

/// <summary>
/// Ensures that an numeric value is positive
/// </summary>
public readonly partial struct Positive : IRefinement<Positive, int>
{
    bool IRefinement<Positive, int>.CanBeRefined(int value) => value > 0;

    string IRefinement<Positive, int>.BuildFailureMessage(int value) =>
        $"Value must be positive, but found {value}";
}
