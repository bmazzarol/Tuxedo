using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a string value is a valid <see cref="Guid"/>
/// </summary>
public readonly struct Uuid : IRefinement<Uuid, string, Guid>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value, [NotNullWhen(false)] out string? failureMessage) =>
        TryRefine(value, out _, out failureMessage);

    /// <inheritdoc />
    public bool TryRefine(
        string value,
        out Guid refinedValue,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (Guid.TryParse(value, out refinedValue))
        {
            failureMessage = null;
            return true;
        }

        failureMessage = "Value must be a valid GUID";
        return false;
    }
}
