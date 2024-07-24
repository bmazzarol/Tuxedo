namespace Tuxedo;

internal static class PositiveRefinements
{
    [Refinement("Number must be positive, but found {value}")]
    internal static bool Positive(sbyte value) => value > 0;

    [Refinement("Number must be positive, but found {value}")]
    internal static bool Positive(short value) => value > 0;

    [Refinement("Number must be positive, but found {value}")]
    internal static bool Positive(int value) => value > 0;

    [Refinement("Number must be positive, but found {value}")]
    internal static bool Positive(long value) => value > 0;

    [Refinement("Number must be positive, but found {value}")]
    internal static bool Positive(byte value) => value > 0;

    [Refinement("Number must be positive, but found {value}")]
    internal static bool Positive(ushort value) => value > 0;

    [Refinement("Number must be positive, but found {value}")]
    internal static bool Positive(uint value) => value > 0;

    [Refinement("Number must be positive, but found {value}")]
    internal static bool Positive(ulong value) => value > 0;

    [Refinement("Number must be positive, but found {value}")]
    internal static bool Positive(float value) => value > 0;

    [Refinement("Number must be positive, but found {value}")]
    internal static bool Positive(double value) => value > 0;

    [Refinement("Number must be positive, but found {value}")]
    internal static bool Positive(decimal value) => value > 0;
}
