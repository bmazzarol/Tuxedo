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
    public bool CanBeRefined(string value) => TryRefine(value, out _);

    /// <inheritdoc />
    public bool TryRefine(string value, out MatchCollection refinedValue)
    {
        refinedValue = Regex.Matches(
            value,
            default(TRegex).Value,
            RegexOptions.None,
            TimeSpan.FromSeconds(30)
        );
        return refinedValue.Count > 0;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(string value) =>
        $"Value must match the regular expression '{default(TRegex).Value}'";
}
