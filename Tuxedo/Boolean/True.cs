namespace Tuxedo;

/// <summary>
/// Enforces that a boolean value is true
/// </summary>
public readonly struct True : IRefinement<True, bool>
{
    /// <inheritdoc />
    public bool CanBeRefined(bool value) => value;

    /// <inheritdoc />
    public bool TryApplyRefinement(bool value, out bool refinedValue)
    {
        refinedValue = value;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(bool value) => "Value must be true";
}
