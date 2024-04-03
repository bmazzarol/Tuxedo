namespace Tuxedo;

/// <summary>
/// Enforces that an numeric value is even
/// </summary>
public readonly partial struct Even : IRefinement<Even, short>
{
    bool IRefinement<Even, short>.CanBeRefined(short value) => value % 2 == 0;

    string IRefinement<Even, short>.BuildFailureMessage(short value) =>
        $"Value must be an even number, but found {value}";
}
