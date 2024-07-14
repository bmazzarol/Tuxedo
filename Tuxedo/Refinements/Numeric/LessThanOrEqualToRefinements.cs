namespace Tuxedo;

/// <summary>
/// Refinements to ensure that a numeric value is less than or equal to another numeric value
/// </summary>
public static class LessThanOrEqualToRefinements
{
    /// <summary>
    /// Ensures that a numeric value is less than or equal to another numeric value
    /// </summary>
    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    public static bool LessThanOrEqualTo<T>(sbyte value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    /// <summary>
    /// Ensures that a numeric value is less than or equal to another numeric value
    /// </summary>
    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    public static bool LessThanOrEqualTo<T>(short value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    /// <summary>
    /// Ensures that a numeric value is less than or equal to another numeric value
    /// </summary>
    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    public static bool LessThanOrEqualTo<T>(int value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    /// <summary>
    /// Ensures that a numeric value is less than or equal to another numeric value
    /// </summary>
    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    public static bool LessThanOrEqualTo<T>(long value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    /// <summary>
    /// Ensures that a numeric value is less than or equal to another numeric value
    /// </summary>
    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    public static bool LessThanOrEqualTo<T>(byte value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    /// <summary>
    /// Ensures that a numeric value is less than or equal to another numeric value
    /// </summary>
    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    public static bool LessThanOrEqualTo<T>(ushort value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    /// <summary>
    /// Ensures that a numeric value is less than or equal to another numeric value
    /// </summary>
    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    public static bool LessThanOrEqualTo<T>(uint value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    /// <summary>
    /// Ensures that a numeric value is less than or equal to another numeric value
    /// </summary>
    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    public static bool LessThanOrEqualTo<T>(ulong value)
        where T : struct, IConstant<T, long> => value <= (ulong)default(T).Value;

    /// <summary>
    /// Ensures that a numeric value is less than or equal to another numeric value
    /// </summary>
    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    public static bool LessThanOrEqualTo<T>(float value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    /// <summary>
    /// Ensures that a numeric value is less than or equal to another numeric value
    /// </summary>
    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    public static bool LessThanOrEqualTo<T>(double value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    /// <summary>
    /// Ensures that a numeric value is less than or equal to another numeric value
    /// </summary>
    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    public static bool LessThanOrEqualTo<T>(decimal value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;
}
