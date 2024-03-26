using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Ensures that a string value has no leading or trailing whitespace
/// </summary>
public readonly struct Trimmed : IRefinementResult<Trimmed, string>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value) =>
        value is string s && s.Equals(s.Trim(), StringComparison.Ordinal);

    /// <inheritdoc />
    public bool TryRefine<TIn>(TIn value, [NotNullWhen(true)] out string? refinedValue)
    {
        if (value is string s)
        {
            refinedValue = s.Trim();
            return true;
        }

        refinedValue = null;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value) =>
        "Value must have no leading or trailing whitespace";
}
