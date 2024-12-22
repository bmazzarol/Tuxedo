using System.Text.RegularExpressions;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces that a given string matches the provided <see cref="Regex"/>
/// </summary>
/// <typeparam name="TRegex">regex</typeparam>
public readonly struct Matching<TRegex>
    : IRefinement<Matching<TRegex>, string>,
        IRefinement<Matching<TRegex>, string, MatchCollection>
    where TRegex : IConstant<TRegex, Regex>
{
    static Matching<TRegex> IRefinement<Matching<TRegex>, string>.Value { get; }

    bool IRefinement<Matching<TRegex>, string>.IsRefined(string value)
    {
        return TRegex.Value.IsMatch(value);
    }

    static Matching<TRegex> IRefinement<Matching<TRegex>, string, MatchCollection>.Value { get; }

    bool IRefinement<Matching<TRegex>, string, MatchCollection>.IsRefined(
        string value,
        out MatchCollection refinedValue
    )
    {
        refinedValue = TRegex.Value.Matches(value);
        return refinedValue.Count > 0;
    }

    string IRefinement<Matching<TRegex>, string, MatchCollection>.BuildFailureMessage(string value)
    {
        return $"Value '{value}' does not match the regex '{TRegex.Value}'";
    }

    string IRefinement<Matching<TRegex>, string>.BuildFailureMessage(string value)
    {
        return $"Value '{value}' does not match the regex '{TRegex.Value}'";
    }
}
