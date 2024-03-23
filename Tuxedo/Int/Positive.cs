namespace Tuxedo;

/// <summary>
/// Ensures that an integer value is positive
/// </summary>
public readonly struct Positive : IRefinement<Positive, int>
{
    /// <inheritdoc />
    public bool CanBeRefined(int value) => value > 0;

    /// <inheritdoc />
    public bool TryApplyRefinement(int value, out int refinedValue)
    {
        refinedValue = value;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(int value) => "Value must be positive";
}
