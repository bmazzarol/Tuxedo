namespace Tuxedo;

internal static class GreaterThanOrEqualToRefinements
{
    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThanOrEqualTo<T>(sbyte value)
        where T : struct, IConstant<T, long> => value >= default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThanOrEqualTo<T>(short value)
        where T : struct, IConstant<T, long> => value >= default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThanOrEqualTo<T>(int value)
        where T : struct, IConstant<T, long> => value >= default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThanOrEqualTo<T>(long value)
        where T : struct, IConstant<T, long> => value >= default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThanOrEqualTo<T>(byte value)
        where T : struct, IConstant<T, long> => value >= default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThanOrEqualTo<T>(ushort value)
        where T : struct, IConstant<T, long> => value >= default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThanOrEqualTo<T>(uint value)
        where T : struct, IConstant<T, long> => value >= default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThanOrEqualTo<T>(ulong value)
        where T : struct, IConstant<T, long> => value >= (ulong)default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThanOrEqualTo<T>(float value)
        where T : struct, IConstant<T, long> => value >= default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThanOrEqualTo<T>(double value)
        where T : struct, IConstant<T, long> => value >= default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThanOrEqualTo<T>(decimal value)
        where T : struct, IConstant<T, long> => value >= default(T).Value;
}
