using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that an numeric value is even
/// </summary>
public readonly partial struct Even : IRefinement<Even, float>
{
    bool IRefinement<Even, float>.CanBeRefined(
        float value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (Math.Abs(value % 2) - 0 < float.Epsilon)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be an even number, but found {value}";
        return false;
    }
}
