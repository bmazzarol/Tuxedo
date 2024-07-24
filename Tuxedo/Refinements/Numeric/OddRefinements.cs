namespace Tuxedo;

internal static class OddRefinements
{
    [Refinement("Number must be an odd number, but found {value}")]
    internal static bool Odd(sbyte value) => value % 2 != 0;

    [Refinement("Number must be an odd number, but found {value}")]
    internal static bool Odd(short value) => value % 2 != 0;

    [Refinement("Number must be an odd number, but found {value}")]
    internal static bool Odd(int value) => value % 2 != 0;

    [Refinement("Number must be an odd number, but found {value}")]
    internal static bool Odd(long value) => value % 2 != 0;

    [Refinement("Number must be an odd number, but found {value}")]
    internal static bool Odd(byte value) => value % 2 != 0;

    [Refinement("Number must be an odd number, but found {value}")]
    internal static bool Odd(ushort value) => value % 2 != 0;

    [Refinement("Number must be an odd number, but found {value}")]
    internal static bool Odd(uint value) => value % 2 != 0;

    [Refinement("Number must be an odd number, but found {value}")]
    internal static bool Odd(ulong value) => value % 2 != 0;
}
