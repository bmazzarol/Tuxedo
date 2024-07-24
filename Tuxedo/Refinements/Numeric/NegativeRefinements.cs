namespace Tuxedo;

internal static class NegativeRefinements
{
    [Refinement("Number must be negative, but found {value}")]
    internal static bool Negative(sbyte value) => value < 0;

    [Refinement("Number must be negative, but found {value}")]
    internal static bool Negative(short value) => value < 0;

    [Refinement("Number must be negative, but found {value}")]
    internal static bool Negative(int value) => value < 0;

    [Refinement("Number must be negative, but found {value}")]
    internal static bool Negative(long value) => value < 0;

    [Refinement("Number must be negative, but found {value}")]
    internal static bool Negative(float value) => value < 0;

    [Refinement("Number must be negative, but found {value}")]
    internal static bool Negative(double value) => value < 0;

    [Refinement("Number must be negative, but found {value}")]
    internal static bool Negative(decimal value) => value < 0;
}
