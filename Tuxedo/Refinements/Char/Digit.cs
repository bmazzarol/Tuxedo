namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces that a character is a digit
/// </summary>
public readonly record struct Digit : IRefinement<Digit, char>
{
    static Digit IRefinement<Digit, char>.Value { get; }

    bool IRefinement<Digit, char>.IsRefined(char value)
    {
        return char.IsDigit(value);
    }

    string IRefinement<Digit, char>.BuildFailureMessage(char value)
    {
        return $"Character must be a digit, but was '{value}'";
    }
}
