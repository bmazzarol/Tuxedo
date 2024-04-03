namespace Tuxedo;

/// <summary>
/// Enforces that an numeric value is even
/// </summary>
public readonly partial struct Even : IRefinement<Even, float>
{
    bool IRefinement<Even, float>.CanBeRefined(float value) =>
        Math.Abs(value % 2) - 0 < float.Epsilon;

    string IRefinement<Even, float>.BuildFailureMessage(float value) =>
        $"Value must be an even number, but found {value}";
}
