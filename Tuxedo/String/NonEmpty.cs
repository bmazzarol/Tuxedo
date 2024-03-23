using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a string value is not be empty
/// </summary>
public readonly struct NonEmpty : IRefinement<NonEmpty, string>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value) => !string.IsNullOrEmpty(value);

    /// <inheritdoc />
    public bool TryApplyRefinement(string value, [NotNullWhen(true)] out string? refinedValue)
    {
        refinedValue = value;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(string value) => "Value cannot be empty";
}
