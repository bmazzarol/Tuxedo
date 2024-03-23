using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Ensures that a string value has no leading or trailing whitespace
/// </summary>
public readonly struct Trimmed : IRefinement<Trimmed, string>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value) =>
        string.Equals(value.Trim(), value, StringComparison.Ordinal);

    /// <inheritdoc />
    public bool TryApplyRefinement(string value, [NotNullWhen(true)] out string? refinedValue)
    {
        refinedValue = value.Trim();
        return true;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(string value) =>
        "Value must have no leading or trailing whitespace";
}
