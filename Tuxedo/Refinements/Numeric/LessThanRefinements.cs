namespace Tuxedo;

internal static class LessThanRefinements
{
    [Refinement("Number must be less than {default(T).Value}, but found {value}")]
    internal static bool LessThan<T>(sbyte value)
        where T : struct, IConstant<T, long> => value < default(T).Value;

    [Refinement("Number must be less than {default(T).Value}, but found {value}")]
    internal static bool LessThan<T>(short value)
        where T : struct, IConstant<T, long> => value < default(T).Value;

    [Refinement("Number must be less than {default(T).Value}, but found {value}")]
    internal static bool LessThan<T>(int value)
        where T : struct, IConstant<T, long> => value < default(T).Value;

    [Refinement("Number must be less than {default(T).Value}, but found {value}")]
    internal static bool LessThan<T>(long value)
        where T : struct, IConstant<T, long> => value < default(T).Value;

    [Refinement("Number must be less than {default(T).Value}, but found {value}")]
    internal static bool LessThan<T>(byte value)
        where T : struct, IConstant<T, long> => value < default(T).Value;

    [Refinement("Number must be less than {default(T).Value}, but found {value}")]
    internal static bool LessThan<T>(ushort value)
        where T : struct, IConstant<T, long> => value < default(T).Value;

    [Refinement("Number must be less than {default(T).Value}, but found {value}")]
    internal static bool LessThan<T>(uint value)
        where T : struct, IConstant<T, long> => value < default(T).Value;

    [Refinement("Number must be less than {default(T).Value}, but found {value}")]
    internal static bool LessThan<T>(ulong value)
        where T : struct, IConstant<T, long> => value < (ulong)default(T).Value;

    [Refinement("Number must be less than {default(T).Value}, but found {value}")]
    internal static bool LessThan<T>(float value)
        where T : struct, IConstant<T, long> => value < default(T).Value;

    [Refinement("Number must be less than {default(T).Value}, but found {value}")]
    internal static bool LessThan<T>(double value)
        where T : struct, IConstant<T, long> => value < default(T).Value;

    [Refinement("Number must be less than {default(T).Value}, but found {value}")]
    internal static bool LessThan<T>(decimal value)
        where T : struct, IConstant<T, long> => value < default(T).Value;
}
