﻿namespace Tuxedo;

/// <summary>
/// Enforces that a string value is a valid <see cref="Guid"/>
/// </summary>
public readonly struct Uuid : IRefinement<Uuid, string, Guid>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value) => TryRefine(value, out _);

    /// <inheritdoc />
    public bool TryRefine(string value, out Guid refinedValue) =>
        Guid.TryParse(value, out refinedValue);

    /// <inheritdoc />
    public string BuildFailureMessage(string value) => "Value must be a valid GUID";
}
