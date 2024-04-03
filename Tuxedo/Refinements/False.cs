namespace Tuxedo;

/// <summary>
/// Enforces that a boolean value is false
/// </summary>
public readonly struct False : IRefinement<False, bool>
{
    /// <inheritdoc />
    public bool CanBeRefined(bool value) => !value;

    /// <inheritdoc />
    public string BuildFailureMessage(bool value) => "Value must be false";
}
