namespace Tuxedo;

internal static class LessThanOrEqualToRefinements
{
    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    internal static bool LessThanOrEqualTo<T>(sbyte value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    internal static bool LessThanOrEqualTo<T>(short value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    internal static bool LessThanOrEqualTo<T>(int value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    internal static bool LessThanOrEqualTo<T>(long value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    internal static bool LessThanOrEqualTo<T>(byte value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    internal static bool LessThanOrEqualTo<T>(ushort value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    internal static bool LessThanOrEqualTo<T>(uint value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    internal static bool LessThanOrEqualTo<T>(ulong value)
        where T : struct, IConstant<T, long> => value <= (ulong)default(T).Value;

    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    internal static bool LessThanOrEqualTo<T>(float value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    internal static bool LessThanOrEqualTo<T>(double value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;

    [Refinement("Number must be less than or equal to {default(T).Value}, but found {value}")]
    internal static bool LessThanOrEqualTo<T>(decimal value)
        where T : struct, IConstant<T, long> => value <= default(T).Value;
}
