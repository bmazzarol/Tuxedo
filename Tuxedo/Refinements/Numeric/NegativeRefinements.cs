namespace Tuxedo;

/// <summary>
/// Refinements to ensure that a numeric value is negative
/// </summary>
public static class NegativeRefinements
{
    /// <summary>
    /// Enforces that a numeric value is negative
    /// </summary>
    [Refinement("Number must be negative, but found {value}")]
    public static bool Negative(sbyte value) => value < 0;

    /// <summary>
    /// Enforces that a numeric value is negative
    /// </summary>
    [Refinement("Number must be negative, but found {value}")]
    public static bool Negative(short value) => value < 0;

    /// <summary>
    /// Enforces that a numeric value is negative
    /// </summary>
    [Refinement("Number must be negative, but found {value}")]
    public static bool Negative(int value) => value < 0;

    /// <summary>
    /// Enforces that a numeric value is negative
    /// </summary>
    [Refinement("Number must be negative, but found {value}")]
    public static bool Negative(long value) => value < 0;

    /// <summary>
    /// Enforces that a numeric value is negative
    /// </summary>
    [Refinement("Number must be negative, but found {value}")]
    public static bool Negative(float value) => value < 0;

    /// <summary>
    /// Enforces that a numeric value is negative
    /// </summary>
    [Refinement("Number must be negative, but found {value}")]
    public static bool Negative(double value) => value < 0;

    /// <summary>
    /// Enforces that a numeric value is negative
    /// </summary>
    [Refinement("Number must be negative, but found {value}")]
    public static bool Negative(decimal value) => value < 0;
}
