using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a boolean value is true
/// </summary>
public readonly struct True : IRefinement<True, bool>
{
    /// <inheritdoc />
    public bool CanBeRefined(bool value, [NotNullWhen(false)] out string? failureMessage)
    {
        if (value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = "Value must be true";
        return false;
    }
}
