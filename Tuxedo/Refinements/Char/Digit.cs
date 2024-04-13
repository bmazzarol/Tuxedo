using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a character is a digit
/// </summary>
public readonly struct Digit : IRefinement<Digit, char>
{
    /// <inheritdoc />
    public bool CanBeRefined(char value, [NotNullWhen(false)] out string? failureMessage)
    {
        if (char.IsDigit(value))
        {
            failureMessage = null;
            return true;
        }
        failureMessage = $"Value must be a digit, instead found '{value}'";
        return false;
    }
}
