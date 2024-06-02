namespace Tuxedo;

public static partial class NaturalNumber
{
    /// <summary>
    /// Term level operations for natural numbers
    /// </summary>
    public static class Operations
    {
        /// <summary>
        /// Adds two natural numbers
        /// </summary>
        public readonly struct Add<TA, TB> : IConstant<Add<TA, TB>, long>
            where TA : struct, IConstant<TA, long>
            where TB : struct, IConstant<TB, long>
        {
            long IConstant<Add<TA, TB>, long>.Value => default(TA).Value + default(TB).Value;
        }

        /// <summary>
        /// Subtracts two natural numbers
        /// </summary>
        public readonly struct Subtract<TA, TB> : IConstant<Subtract<TA, TB>, long>
            where TA : struct, IConstant<TA, long>
            where TB : struct, IConstant<TB, long>
        {
            long IConstant<Subtract<TA, TB>, long>.Value => default(TA).Value - default(TB).Value;
        }

        /// <summary>
        /// Multiplies two natural numbers
        /// </summary>
        public readonly struct Multiply<TA, TB> : IConstant<Multiply<TA, TB>, long>
            where TA : struct, IConstant<TA, long>
            where TB : struct, IConstant<TB, long>
        {
            long IConstant<Multiply<TA, TB>, long>.Value => default(TA).Value * default(TB).Value;
        }

        /// <summary>
        /// Divides two natural numbers
        /// </summary>
        public readonly struct Divide<TA, TB> : IConstant<Divide<TA, TB>, long>
            where TA : struct, IConstant<TA, long>
            where TB : struct, IConstant<TB, long>
        {
            long IConstant<Divide<TA, TB>, long>.Value => default(TA).Value / default(TB).Value;
        }

        /// <summary>
        /// Modulo of two natural numbers
        /// </summary>
        public readonly struct Modulo<TA, TB> : IConstant<Modulo<TA, TB>, long>
            where TA : struct, IConstant<TA, long>
            where TB : struct, IConstant<TB, long>
        {
            long IConstant<Modulo<TA, TB>, long>.Value => default(TA).Value % default(TB).Value;
        }

        /// <summary>
        /// Negates a natural number
        /// </summary>
        public readonly struct Negate<TA> : IConstant<Negate<TA>, long>
            where TA : struct, IConstant<TA, long>
        {
            long IConstant<Negate<TA>, long>.Value => -default(TA).Value;
        }
    }
}
