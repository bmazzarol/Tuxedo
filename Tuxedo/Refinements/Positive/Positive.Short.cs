namespace Tuxedo;

/// <summary>
/// Ensures that an numeric value is positive
/// </summary>
public readonly partial struct Positive : IRefinement<Positive, short>
{
    bool IRefinement<Positive, short>.CanBeRefined(short value) => value > 0;

    string IRefinement<Positive, short>.BuildFailureMessage(short value) =>
        $"Value must be positive, but found {value}";
}
