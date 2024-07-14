namespace Tuxedo;

/// <summary>
/// Refinements to ensure that a numeric value is positive
/// </summary>
public static class PositiveRefinements
{
    /// <summary>
    /// Enforces that a numeric value is positive
    /// </summary>
    [Refinement("Number must be positive, but found {value}")]
    public static bool Positive(sbyte value) => value > 0;

    /// <summary>
    /// Enforces that a numeric value is positive
    /// </summary>
    [Refinement("Number must be positive, but found {value}")]
    public static bool Positive(short value) => value > 0;

    /// <summary>
    /// Enforces that a numeric value is positive
    /// </summary>
    [Refinement("Number must be positive, but found {value}")]
    public static bool Positive(int value) => value > 0;

    /// <summary>
    /// Enforces that a numeric value is positive
    /// </summary>
    [Refinement("Number must be positive, but found {value}")]
    public static bool Positive(long value) => value > 0;

    /// <summary>
    /// Enforces that a numeric value is positive
    /// </summary>
    [Refinement("Number must be positive, but found {value}")]
    public static bool Positive(byte value) => value > 0;

    /// <summary>
    /// Enforces that a numeric value is positive
    /// </summary>
    [Refinement("Number must be positive, but found {value}")]
    public static bool Positive(ushort value) => value > 0;

    /// <summary>
    /// Enforces that a numeric value is positive
    /// </summary>
    [Refinement("Number must be positive, but found {value}")]
    public static bool Positive(uint value) => value > 0;

    /// <summary>
    /// Enforces that a numeric value is positive
    /// </summary>
    [Refinement("Number must be positive, but found {value}")]
    public static bool Positive(ulong value) => value > 0;

    /// <summary>
    /// Enforces that a numeric value is positive
    /// </summary>
    [Refinement("Number must be positive, but found {value}")]
    public static bool Positive(float value) => value > 0;

    /// <summary>
    /// Enforces that a numeric value is positive
    /// </summary>
    [Refinement("Number must be positive, but found {value}")]
    public static bool Positive(double value) => value > 0;

    /// <summary>
    /// Enforces that a numeric value is positive
    /// </summary>
    [Refinement("Number must be positive, but found {value}")]
    public static bool Positive(decimal value) => value > 0;
}
