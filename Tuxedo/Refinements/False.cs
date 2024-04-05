using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a boolean value is false
/// </summary>
public readonly struct False : IRefinement<False, bool>
{
    /// <inheritdoc />
    public bool CanBeRefined(bool value, [NotNullWhen(false)] out string? failureMessage)
    {
        if (!value)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = "Value must be false";
        return false;
    }
}
