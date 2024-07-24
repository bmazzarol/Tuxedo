namespace Tuxedo;

internal static class BetweenRefinements
{
    [Refinement(
        "Number must be between {default(TMin).Value} and {default(TMax).Value}, but found {value}"
    )]
    internal static bool Between<TMin, TMax>(sbyte value)
        where TMin : struct, IConstant<TMin, long>
        where TMax : struct, IConstant<TMax, long> =>
        value >= default(TMin).Value && value <= default(TMax).Value;

    [Refinement(
        "Number must be between {default(TMin).Value} and {default(TMax).Value}, but found {value}"
    )]
    internal static bool Between<TMin, TMax>(short value)
        where TMin : struct, IConstant<TMin, long>
        where TMax : struct, IConstant<TMax, long> =>
        value >= default(TMin).Value && value <= default(TMax).Value;

    [Refinement(
        "Number must be between {default(TMin).Value} and {default(TMax).Value}, but found {value}"
    )]
    internal static bool Between<TMin, TMax>(int value)
        where TMin : struct, IConstant<TMin, long>
        where TMax : struct, IConstant<TMax, long> =>
        value >= default(TMin).Value && value <= default(TMax).Value;

    [Refinement(
        "Number must be between {default(TMin).Value} and {default(TMax).Value}, but found {value}"
    )]
    internal static bool Between<TMin, TMax>(long value)
        where TMin : struct, IConstant<TMin, long>
        where TMax : struct, IConstant<TMax, long> =>
        value >= default(TMin).Value && value <= default(TMax).Value;

    [Refinement(
        "Number must be between {default(TMin).Value} and {default(TMax).Value}, but found {value}"
    )]
    internal static bool Between<TMin, TMax>(byte value)
        where TMin : struct, IConstant<TMin, long>
        where TMax : struct, IConstant<TMax, long> =>
        value >= default(TMin).Value && value <= default(TMax).Value;

    [Refinement(
        "Number must be between {default(TMin).Value} and {default(TMax).Value}, but found {value}"
    )]
    internal static bool Between<TMin, TMax>(ushort value)
        where TMin : struct, IConstant<TMin, long>
        where TMax : struct, IConstant<TMax, long> =>
        value >= default(TMin).Value && value <= default(TMax).Value;

    [Refinement(
        "Number must be between {default(TMin).Value} and {default(TMax).Value}, but found {value}"
    )]
    internal static bool Between<TMin, TMax>(uint value)
        where TMin : struct, IConstant<TMin, long>
        where TMax : struct, IConstant<TMax, long> =>
        value >= default(TMin).Value && value <= default(TMax).Value;

    [Refinement(
        "Number must be between {default(TMin).Value} and {default(TMax).Value}, but found {value}"
    )]
    internal static bool Between<TMin, TMax>(ulong value)
        where TMin : struct, IConstant<TMin, long>
        where TMax : struct, IConstant<TMax, long> =>
        value >= (ulong)default(TMin).Value && value <= (ulong)default(TMax).Value;

    [Refinement(
        "Number must be between {default(TMin).Value} and {default(TMax).Value}, but found {value}"
    )]
    internal static bool Between<TMin, TMax>(float value)
        where TMin : struct, IConstant<TMin, long>
        where TMax : struct, IConstant<TMax, long> =>
        value >= default(TMin).Value && value <= default(TMax).Value;

    [Refinement(
        "Number must be between {default(TMin).Value} and {default(TMax).Value}, but found {value}"
    )]
    internal static bool Between<TMin, TMax>(double value)
        where TMin : struct, IConstant<TMin, long>
        where TMax : struct, IConstant<TMax, long> =>
        value >= default(TMin).Value && value <= default(TMax).Value;

    [Refinement(
        "Number must be between {default(TMin).Value} and {default(TMax).Value}, but found {value}"
    )]
    internal static bool Between<TMin, TMax>(decimal value)
        where TMin : struct, IConstant<TMin, long>
        where TMax : struct, IConstant<TMax, long> =>
        value >= default(TMin).Value && value <= default(TMax).Value;
}
