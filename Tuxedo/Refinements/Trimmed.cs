using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Ensures that a string value has no leading or trailing whitespace
/// </summary>
public readonly struct Trimmed : IRefinement<Trimmed, string, string>
{
    /// <summary>
    /// Determines if the value is trimmed
    /// </summary>
    /// <param name="value">The value to check</param>
    /// <returns>True if the value is trimmed, otherwise false</returns>
    public bool CanBeRefined(string value)
    {
        return string.Equals(value.Trim(), value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Builds a failure message
    /// </summary>
    /// <param name="value">The value that failed the refinement</param>
    /// <returns>A message describing the failure</returns>
    public string BuildFailureMessage(string value) =>
        $"Value must be trimmed, but found '{value}'";

    /// <inheritdoc />
    public bool TryRefine(string value, [NotNullWhen(true)] out string? refinedValue)
    {
        refinedValue = value.Trim();
        return true;
    }
}
