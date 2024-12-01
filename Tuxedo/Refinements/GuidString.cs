using System.Diagnostics.CodeAnalysis;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a value to be a valid GUID
/// </summary>
public sealed class GuidString : Refinement<GuidString, string, Guid>
{
    /// <inheritdoc />
    public override bool CanBeRefined(
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

        failureMessage = $"Value must be a valid GUID, but was {value}";
        return false;
    }
}
