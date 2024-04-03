namespace Tuxedo;

/// <summary>
/// Enforces that an numeric value is even
/// </summary>
public readonly partial struct Even : IRefinement<Even, long>
{
    bool IRefinement<Even, long>.CanBeRefined(long value) => value % 2 == 0;

    string IRefinement<Even, long>.BuildFailureMessage(long value) =>
        $"Value must be an even number, but found {value}";
}
