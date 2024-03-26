namespace Tuxedo;

/// <summary>
/// Enforces that a boolean value is true
/// </summary>
public readonly struct True : IRefinement<True>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value) => value is true;

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value) => "Value must be true";
}
