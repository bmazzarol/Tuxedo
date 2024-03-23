namespace Tuxedo;

/// <summary>
/// Enforces that an integer value is even
/// </summary>
public readonly struct Even : IRefinement<Even, int>
{
    /// <inheritdoc />
    public bool CanBeRefined(int value) => value % 2 == 0;

    /// <inheritdoc />
    public bool TryApplyRefinement(int value, out int refinedValue)
    {
        refinedValue = value;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(int value) => "Value must be even";
}
