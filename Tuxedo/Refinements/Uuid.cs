namespace Tuxedo;

/// <summary>
/// Enforces that a string value is a valid <see cref="Guid"/>
/// </summary>
public readonly struct Uuid : IRefinementResult<Uuid, Guid>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value) => TryRefine(value, out _);

    /// <inheritdoc />
    public bool TryRefine<T>(T value, out Guid refinedValue) =>
        value is string s && Guid.TryParse(s, out refinedValue);

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value) => "Value must be a valid GUID";
}
