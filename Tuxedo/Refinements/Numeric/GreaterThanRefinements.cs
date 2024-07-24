namespace Tuxedo;

internal static class GreaterThanRefinements
{
    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThan<T>(sbyte value)
        where T : struct, IConstant<T, long> => value > default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThan<T>(short value)
        where T : struct, IConstant<T, long> => value > default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThan<T>(int value)
        where T : struct, IConstant<T, long> => value > default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThan<T>(long value)
        where T : struct, IConstant<T, long> => value > default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThan<T>(byte value)
        where T : struct, IConstant<T, long> => value > default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThan<T>(ushort value)
        where T : struct, IConstant<T, long> => value > default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThan<T>(uint value)
        where T : struct, IConstant<T, long> => value > default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThan<T>(ulong value)
        where T : struct, IConstant<T, long> => value > (ulong)default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThan<T>(float value)
        where T : struct, IConstant<T, long> => value > default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThan<T>(double value)
        where T : struct, IConstant<T, long> => value > default(T).Value;

    [Refinement("Number must be greater than {default(T).Value}, but found {value}")]
    internal static bool GreaterThan<T>(decimal value)
        where T : struct, IConstant<T, long> => value > default(T).Value;
}
