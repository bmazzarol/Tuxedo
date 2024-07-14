namespace Tuxedo;

/// <summary>
/// Refinements to ensure that a numeric value is even
/// </summary>
public static class EvenRefinements
{
    /// <summary>
    /// Enforces that a numeric value is even
    /// </summary>
    [Refinement("Number must be an even number, but found {value}")]
    public static bool Even(sbyte value) => value % 2 == 0;

    /// <summary>
    /// Enforces that a numeric value is even
    /// </summary>
    [Refinement("Number must be an even number, but found {value}")]
    public static bool Even(short value) => value % 2 == 0;

    /// <summary>
    /// Enforces that a numeric value is even
    /// </summary>
    [Refinement("Number must be an even number, but found {value}")]
    public static bool Even(int value) => value % 2 == 0;

    /// <summary>
    /// Enforces that a numeric value is even
    /// </summary>
    [Refinement("Number must be an even number, but found {value}")]
    public static bool Even(long value) => value % 2 == 0;

    /// <summary>
    /// Enforces that a numeric value is even
    /// </summary>
    [Refinement("Number must be an even number, but found {value}")]
    public static bool Even(byte value) => value % 2 == 0;

    /// <summary>
    /// Enforces that a numeric value is even
    /// </summary>
    [Refinement("Number must be an even number, but found {value}")]
    public static bool Even(ushort value) => value % 2 == 0;

    /// <summary>
    /// Enforces that a numeric value is even
    /// </summary>
    [Refinement("Number must be an even number, but found {value}")]
    public static bool Even(uint value) => value % 2 == 0;

    /// <summary>
    /// Enforces that a numeric value is even
    /// </summary>
    [Refinement("Number must be an even number, but found {value}")]
    public static bool Even(ulong value) => value % 2 == 0;
}
