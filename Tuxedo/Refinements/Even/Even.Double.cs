namespace Tuxedo;

/// <summary>
/// Enforces that an numeric value is even
/// </summary>
public readonly partial struct Even : IRefinement<Even, double>
{
    bool IRefinement<Even, double>.CanBeRefined(double value) =>
        Math.Abs(value % 2) - 0 < double.Epsilon;

    string IRefinement<Even, double>.BuildFailureMessage(double value) =>
        $"Value must be an even number, but found {value}";
}
