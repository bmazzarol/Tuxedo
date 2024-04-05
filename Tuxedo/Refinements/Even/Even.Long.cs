using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that an numeric value is even
/// </summary>
public readonly partial struct Even : IRefinement<Even, long>
{
    bool IRefinement<Even, long>.CanBeRefined(
        long value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value % 2 == 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be an even number, but found {value}";
        return false;
    }
}
