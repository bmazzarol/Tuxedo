using System.Text.RegularExpressions;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces that a given string matches the provided <see cref="Regex"/>
/// </summary>
/// <typeparam name="TRegex">regex</typeparam>
public readonly struct Matches<TRegex> : IRefinement<Matches<TRegex>, string>
    where TRegex : IConstant<TRegex, Regex>
{
    static Matches<TRegex> IRefinement<Matches<TRegex>, string>.Value { get; }

    bool IRefinement<Matches<TRegex>, string>.IsRefined(string value)
    {
        return TRegex.Value.IsMatch(value);
    }

    string IRefinement<Matches<TRegex>, string>.BuildFailureMessage(string value)
    {
        return $"Value '{value}' does not match the regex '{TRegex.Value}'";
    }
}
