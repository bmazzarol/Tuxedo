namespace Tuxedo;

internal static class EvenRefinements
{
    [Refinement("Number must be an even number, but found {value}")]
    internal static bool Even(sbyte value) => value % 2 == 0;

    [Refinement("Number must be an even number, but found {value}")]
    internal static bool Even(short value) => value % 2 == 0;

    [Refinement("Number must be an even number, but found {value}")]
    internal static bool Even(int value) => value % 2 == 0;

    [Refinement("Number must be an even number, but found {value}")]
    internal static bool Even(long value) => value % 2 == 0;

    [Refinement("Number must be an even number, but found {value}")]
    internal static bool Even(byte value) => value % 2 == 0;

    [Refinement("Number must be an even number, but found {value}")]
    internal static bool Even(ushort value) => value % 2 == 0;

    [Refinement("Number must be an even number, but found {value}")]
    internal static bool Even(uint value) => value % 2 == 0;

    [Refinement("Number must be an even number, but found {value}")]
    internal static bool Even(ulong value) => value % 2 == 0;
}
