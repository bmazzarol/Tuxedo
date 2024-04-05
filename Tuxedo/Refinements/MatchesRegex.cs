using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Tuxedo;

/// <summary>
/// Enforces that a string value matches a regular expression
/// </summary>
/// <typeparam name="TRegex">the regular expression to match</typeparam>
public readonly struct MatchesRegex<TRegex>
    : IRefinement<MatchesRegex<TRegex>, string, MatchCollection>
    where TRegex : struct, IConstant<TRegex, string>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value, [NotNullWhen(false)] out string? failureMessage) =>
        TryRefine(value, out _, out failureMessage);

    /// <inheritdoc />
    public bool TryRefine(
        string value,
        [NotNullWhen(true)] out MatchCollection? refinedValue,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        var matches = Regex.Matches(
            value,
            default(TRegex).Value,
            RegexOptions.None,
            TimeSpan.FromSeconds(30)
        );

        if (matches.Count > 0)
        {
            refinedValue = matches;
            failureMessage = null;
            return true;
        }

        refinedValue = null;
        failureMessage = $"Value must match the regular expression '{default(TRegex).Value}'";
        return false;
    }
}
