namespace Tuxedo;

/// <summary>
/// Refinements for <see cref="char"/>
/// </summary>
public static class CharRefinements
{
    /// <summary>
    /// Enforces that a character is a digit
    /// </summary>
    [Refinement("The character must be a letter, instead found '{value}'")]
    public static bool Letter(char value) => char.IsLetter(value);

    /// <summary>
    /// Enforces that a character is a digit
    /// </summary>
    [Refinement("The character must be a digit, instead found '{value}'")]
    public static bool Digit(char value) => char.IsDigit(value);

    /// <summary>
    /// Enforces that a character is lower case
    /// </summary>
    [Refinement("The character must be lower case, instead found '{value}'")]
    public static bool Lower(char value) => char.IsLower(value);

    /// <summary>
    /// Enforces that a character is upper case
    /// </summary>
    [Refinement("The character must be upper case, instead found '{value}'")]
    public static bool Upper(char value) => char.IsUpper(value);

    /// <summary>
    /// Enforces that a character is a symbol
    /// </summary>
    [Refinement("The character must be a symbol, instead found '{value}'")]
    public static bool Symbol(char value) => char.IsSymbol(value);

    /// <summary>
    /// Enforces that a character is a punctuation
    /// </summary>
    [Refinement("The character must be a punctuation, instead found '{value}'")]
    public static bool Punctuation(char value) => char.IsPunctuation(value);

    /// <summary>
    /// Enforces that a character is a separator
    /// </summary>
    [Refinement("The character must be a separator, instead found '{value}'")]
    public static bool Separator(char value) => char.IsSeparator(value);

    /// <summary>
    /// Enforces that a character is a control
    /// </summary>
    [Refinement("The character must be a control, instead found '{value}'")]
    public static bool Control(char value) => char.IsControl(value);

    /// <summary>
    /// Enforces that a character is a whitespace
    /// </summary>
    [Refinement("The character must be a whitespace, instead found '{value}'")]
    public static bool Whitespace(char value) => char.IsWhiteSpace(value);

    /// <summary>
    /// Enforces that a character is a surrogate
    /// </summary>
    [Refinement("The character must be a surrogate, instead found '{value}'")]
    public static bool Surrogate(char value) => char.IsSurrogate(value);

    /// <summary>
    /// Enforces that a character is a high surrogate
    /// </summary>
    [Refinement("The character must be a high surrogate, instead found '{value}'")]
    public static bool HighSurrogate(char value) => char.IsHighSurrogate(value);

    /// <summary>
    /// Enforces that a character is a low surrogate
    /// </summary>
    [Refinement("The character must be a low surrogate, instead found '{value}'")]
    public static bool LowSurrogate(char value) => char.IsLowSurrogate(value);

    /// <summary>
    /// Enforces that a character is a valid ASCII character
    /// </summary>
    [Refinement("The character must be a valid ASCII character, instead found '{value}'")]
    public static bool Ascii(char value) => value < 128;

    /// <summary>
    /// Enforces that a character is a valid-extended ASCII character
    /// </summary>
    [Refinement("The character must be a valid extended ASCII character, instead found '{value}'")]
    public static bool ExtendedAscii(char value) => value < 256;
}
