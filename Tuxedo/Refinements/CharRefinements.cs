namespace Tuxedo;

internal static class CharRefinements
{
    [Refinement("The character must be a letter, instead found '{value}'")]
    internal static bool Letter(char value) => char.IsLetter(value);

    [Refinement("The character must be a digit, instead found '{value}'")]
    internal static bool Digit(char value) => char.IsDigit(value);

    [Refinement("The character must be lower case, instead found '{value}'")]
    internal static bool Lower(char value) => char.IsLower(value);

    [Refinement("The character must be upper case, instead found '{value}'")]
    internal static bool Upper(char value) => char.IsUpper(value);

    [Refinement("The character must be a symbol, instead found '{value}'")]
    internal static bool Symbol(char value) => char.IsSymbol(value);

    [Refinement("The character must be a punctuation, instead found '{value}'")]
    internal static bool Punctuation(char value) => char.IsPunctuation(value);

    [Refinement("The character must be a separator, instead found '{value}'")]
    internal static bool Separator(char value) => char.IsSeparator(value);

    [Refinement("The character must be a control, instead found '{value}'")]
    internal static bool Control(char value) => char.IsControl(value);

    [Refinement("The character must be a whitespace, instead found '{value}'")]
    internal static bool Whitespace(char value) => char.IsWhiteSpace(value);

    [Refinement("The character must be a surrogate, instead found '{value}'")]
    internal static bool Surrogate(char value) => char.IsSurrogate(value);

    [Refinement("The character must be a high surrogate, instead found '{value}'")]
    internal static bool HighSurrogate(char value) => char.IsHighSurrogate(value);

    [Refinement("The character must be a low surrogate, instead found '{value}'")]
    internal static bool LowSurrogate(char value) => char.IsLowSurrogate(value);

    [Refinement("The character must be a valid ASCII character, instead found '{value}'")]
    internal static bool Ascii(char value) => value < 128;

    [Refinement("The character must be a valid extended ASCII character, instead found '{value}'")]
    internal static bool ExtendedAscii(char value) => value < 256;
}
