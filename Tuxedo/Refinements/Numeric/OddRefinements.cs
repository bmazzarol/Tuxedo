namespace Tuxedo;

/// <summary>
/// Refinements to ensure that a numeric value is odd
/// </summary>
public static class OddRefinements
{
    /// <summary>
    /// Enforces that a numeric value is odd
    /// </summary>
    [Refinement("Number must be an odd number, but found {value}")]
    public static bool Odd(sbyte value) => value % 2 != 0;

    /// <summary>
    /// Enforces that a numeric value is odd
    /// </summary>
    [Refinement("Number must be an odd number, but found {value}")]
    public static bool Odd(short value) => value % 2 != 0;

    /// <summary>
    /// Enforces that a numeric value is odd
    /// </summary>
    [Refinement("Number must be an odd number, but found {value}")]
    public static bool Odd(int value) => value % 2 != 0;

    /// <summary>
    /// Enforces that a numeric value is odd
    /// </summary>
    [Refinement("Number must be an odd number, but found {value}")]
    public static bool Odd(long value) => value % 2 != 0;

    /// <summary>
    /// Enforces that a numeric value is odd
    /// </summary>
    [Refinement("Number must be an odd number, but found {value}")]
    public static bool Odd(byte value) => value % 2 != 0;

    /// <summary>
    /// Enforces that a numeric value is odd
    /// </summary>
    [Refinement("Number must be an odd number, but found {value}")]
    public static bool Odd(ushort value) => value % 2 != 0;

    /// <summary>
    /// Enforces that a numeric value is odd
    /// </summary>
    [Refinement("Number must be an odd number, but found {value}")]
    public static bool Odd(uint value) => value % 2 != 0;

    /// <summary>
    /// Enforces that a numeric value is odd
    /// </summary>
    [Refinement("Number must be an odd number, but found {value}")]
    public static bool Odd(ulong value) => value % 2 != 0;
}
