namespace Tuxedo;

/// <summary>
/// Enforces that a boolean value is false
/// </summary>
public readonly struct False : IRefinement<False>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value) => value is false;

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value) => "Value must be false";
}
