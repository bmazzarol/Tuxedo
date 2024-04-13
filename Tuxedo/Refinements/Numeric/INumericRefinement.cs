namespace Tuxedo;

/// <summary>
/// Refinement for numeric values
/// </summary>
public interface INumericRefinement<TThis>
    : ISignedNumericRefinement<TThis>,
        IUnsignedNumericRefinement<TThis>,
        IFloatingPointNumericRefinement<TThis>
    where TThis : struct;

/// <summary>
/// Refinement for signed numeric values
/// </summary>
public interface ISignedNumericRefinement<TThis>
    : IRefinement<TThis, sbyte>,
        IRefinement<TThis, short>,
        IRefinement<TThis, int>,
        IRefinement<TThis, long>
    where TThis : struct;

/// <summary>
/// Refinement for unsigned numeric values
/// </summary>
public interface IUnsignedNumericRefinement<TThis>
    : IRefinement<TThis, byte>,
        IRefinement<TThis, ushort>,
        IRefinement<TThis, uint>,
        IRefinement<TThis, ulong>
    where TThis : struct;

/// <summary>
/// Refinement for floating point numeric values
/// </summary>
public interface IFloatingPointNumericRefinement<TThis>
    : IRefinement<TThis, float>,
        IRefinement<TThis, double>,
        IRefinement<TThis, decimal>
    where TThis : struct;
