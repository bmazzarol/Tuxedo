using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Ensures that a string value has no leading or trailing whitespace
/// </summary>
public readonly struct Trimmed : IRefinement<Trimmed, string, string>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value, [NotNullWhen(false)] out string? failureMessage)
    {
        if (string.Equals(value.Trim(), value, StringComparison.Ordinal))
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be trimmed, but found '{value}'";
        return false;
    }

    /// <inheritdoc />
    public bool TryRefine(
        string value,
        [NotNullWhen(true)] out string? refinedValue,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        refinedValue = value.Trim();
        failureMessage = null;
        return true;
    }
}
