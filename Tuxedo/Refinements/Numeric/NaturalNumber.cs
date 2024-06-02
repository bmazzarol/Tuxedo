#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// The natural numbers to use in refinements
/// </summary>
[ExcludeFromCodeCoverage]
public static partial class NaturalNumber
{
    public readonly struct Zero : IConstant<Zero, long>
    {
        long IConstant<Zero, long>.Value => 0;
    }

    public readonly struct One : IConstant<One, long>
    {
        long IConstant<One, long>.Value => 1;
    }

    public readonly struct Two : IConstant<Two, long>
    {
        long IConstant<Two, long>.Value => 2;
    }

    public readonly struct Three : IConstant<Three, long>
    {
        long IConstant<Three, long>.Value => 3;
    }

    public readonly struct Four : IConstant<Four, long>
    {
        long IConstant<Four, long>.Value => 4;
    }

    public readonly struct Five : IConstant<Five, long>
    {
        long IConstant<Five, long>.Value => 5;
    }

    public readonly struct Six : IConstant<Six, long>
    {
        long IConstant<Six, long>.Value => 6;
    }

    public readonly struct Seven : IConstant<Seven, long>
    {
        long IConstant<Seven, long>.Value => 7;
    }

    public readonly struct Eight : IConstant<Eight, long>
    {
        long IConstant<Eight, long>.Value => 8;
    }

    public readonly struct Nine : IConstant<Nine, long>
    {
        long IConstant<Nine, long>.Value => 9;
    }

    public readonly struct Ten : IConstant<Ten, long>
    {
        long IConstant<Ten, long>.Value => 10;
    }

    public readonly struct Eleven : IConstant<Eleven, long>
    {
        long IConstant<Eleven, long>.Value => 11;
    }

    public readonly struct Twelve : IConstant<Twelve, long>
    {
        long IConstant<Twelve, long>.Value => 12;
    }

    public readonly struct Thirteen : IConstant<Thirteen, long>
    {
        long IConstant<Thirteen, long>.Value => 13;
    }

    public readonly struct Fourteen : IConstant<Fourteen, long>
    {
        long IConstant<Fourteen, long>.Value => 14;
    }

    public readonly struct Fifteen : IConstant<Fifteen, long>
    {
        long IConstant<Fifteen, long>.Value => 15;
    }

    public readonly struct Sixteen : IConstant<Sixteen, long>
    {
        long IConstant<Sixteen, long>.Value => 16;
    }

    public readonly struct Seventeen : IConstant<Seventeen, long>
    {
        long IConstant<Seventeen, long>.Value => 17;
    }

    public readonly struct Eighteen : IConstant<Eighteen, long>
    {
        long IConstant<Eighteen, long>.Value => 18;
    }

    public readonly struct Nineteen : IConstant<Nineteen, long>
    {
        long IConstant<Nineteen, long>.Value => 19;
    }

    public readonly struct Twenty : IConstant<Twenty, long>
    {
        long IConstant<Twenty, long>.Value => 20;
    }

    public readonly struct TwentyOne : IConstant<TwentyOne, long>
    {
        long IConstant<TwentyOne, long>.Value => 21;
    }

    public readonly struct TwentyTwo : IConstant<TwentyTwo, long>
    {
        long IConstant<TwentyTwo, long>.Value => 22;
    }

    public readonly struct TwentyThree : IConstant<TwentyThree, long>
    {
        long IConstant<TwentyThree, long>.Value => 23;
    }

    public readonly struct TwentyFour : IConstant<TwentyFour, long>
    {
        long IConstant<TwentyFour, long>.Value => 24;
    }

    public readonly struct TwentyFive : IConstant<TwentyFive, long>
    {
        long IConstant<TwentyFive, long>.Value => 25;
    }

    public readonly struct TwentySix : IConstant<TwentySix, long>
    {
        long IConstant<TwentySix, long>.Value => 26;
    }

    public readonly struct TwentySeven : IConstant<TwentySeven, long>
    {
        long IConstant<TwentySeven, long>.Value => 27;
    }

    public readonly struct TwentyEight : IConstant<TwentyEight, long>
    {
        long IConstant<TwentyEight, long>.Value => 28;
    }

    public readonly struct TwentyNine : IConstant<TwentyNine, long>
    {
        long IConstant<TwentyNine, long>.Value => 29;
    }

    public readonly struct Thirty : IConstant<Thirty, long>
    {
        long IConstant<Thirty, long>.Value => 30;
    }

    public readonly struct ThirtyOne : IConstant<ThirtyOne, long>
    {
        long IConstant<ThirtyOne, long>.Value => 31;
    }

    public readonly struct ThirtyTwo : IConstant<ThirtyTwo, long>
    {
        long IConstant<ThirtyTwo, long>.Value => 32;
    }

    public readonly struct ThirtyThree : IConstant<ThirtyThree, long>
    {
        long IConstant<ThirtyThree, long>.Value => 33;
    }

    public readonly struct ThirtyFour : IConstant<ThirtyFour, long>
    {
        long IConstant<ThirtyFour, long>.Value => 34;
    }

    public readonly struct ThirtyFive : IConstant<ThirtyFive, long>
    {
        long IConstant<ThirtyFive, long>.Value => 35;
    }

    public readonly struct ThirtySix : IConstant<ThirtySix, long>
    {
        long IConstant<ThirtySix, long>.Value => 36;
    }

    public readonly struct ThirtySeven : IConstant<ThirtySeven, long>
    {
        long IConstant<ThirtySeven, long>.Value => 37;
    }

    public readonly struct ThirtyEight : IConstant<ThirtyEight, long>
    {
        long IConstant<ThirtyEight, long>.Value => 38;
    }

    public readonly struct ThirtyNine : IConstant<ThirtyNine, long>
    {
        long IConstant<ThirtyNine, long>.Value => 39;
    }

    public readonly struct Forty : IConstant<Forty, long>
    {
        long IConstant<Forty, long>.Value => 40;
    }

    public readonly struct FortyOne : IConstant<FortyOne, long>
    {
        long IConstant<FortyOne, long>.Value => 41;
    }

    public readonly struct FortyTwo : IConstant<FortyTwo, long>
    {
        long IConstant<FortyTwo, long>.Value => 42;
    }

    public readonly struct FortyThree : IConstant<FortyThree, long>
    {
        long IConstant<FortyThree, long>.Value => 43;
    }

    public readonly struct FortyFour : IConstant<FortyFour, long>
    {
        long IConstant<FortyFour, long>.Value => 44;
    }

    public readonly struct FortyFive : IConstant<FortyFive, long>
    {
        long IConstant<FortyFive, long>.Value => 45;
    }

    public readonly struct FortySix : IConstant<FortySix, long>
    {
        long IConstant<FortySix, long>.Value => 46;
    }

    public readonly struct FortySeven : IConstant<FortySeven, long>
    {
        long IConstant<FortySeven, long>.Value => 47;
    }

    public readonly struct FortyEight : IConstant<FortyEight, long>
    {
        long IConstant<FortyEight, long>.Value => 48;
    }

    public readonly struct FortyNine : IConstant<FortyNine, long>
    {
        long IConstant<FortyNine, long>.Value => 49;
    }

    public readonly struct Fifty : IConstant<Fifty, long>
    {
        long IConstant<Fifty, long>.Value => 50;
    }

    public readonly struct FiftyOne : IConstant<FiftyOne, long>
    {
        long IConstant<FiftyOne, long>.Value => 51;
    }

    public readonly struct FiftyTwo : IConstant<FiftyTwo, long>
    {
        long IConstant<FiftyTwo, long>.Value => 52;
    }

    public readonly struct FiftyThree : IConstant<FiftyThree, long>
    {
        long IConstant<FiftyThree, long>.Value => 53;
    }

    public readonly struct FiftyFour : IConstant<FiftyFour, long>
    {
        long IConstant<FiftyFour, long>.Value => 54;
    }

    public readonly struct FiftyFive : IConstant<FiftyFive, long>
    {
        long IConstant<FiftyFive, long>.Value => 55;
    }

    public readonly struct FiftySix : IConstant<FiftySix, long>
    {
        long IConstant<FiftySix, long>.Value => 56;
    }

    public readonly struct FiftySeven : IConstant<FiftySeven, long>
    {
        long IConstant<FiftySeven, long>.Value => 57;
    }

    public readonly struct FiftyEight : IConstant<FiftyEight, long>
    {
        long IConstant<FiftyEight, long>.Value => 58;
    }

    public readonly struct FiftyNine : IConstant<FiftyNine, long>
    {
        long IConstant<FiftyNine, long>.Value => 59;
    }

    public readonly struct Sixty : IConstant<Sixty, long>
    {
        long IConstant<Sixty, long>.Value => 60;
    }

    public readonly struct SixtyOne : IConstant<SixtyOne, long>
    {
        long IConstant<SixtyOne, long>.Value => 61;
    }

    public readonly struct SixtyTwo : IConstant<SixtyTwo, long>
    {
        long IConstant<SixtyTwo, long>.Value => 62;
    }

    public readonly struct SixtyThree : IConstant<SixtyThree, long>
    {
        long IConstant<SixtyThree, long>.Value => 63;
    }

    public readonly struct SixtyFour : IConstant<SixtyFour, long>
    {
        long IConstant<SixtyFour, long>.Value => 64;
    }

    public readonly struct SixtyFive : IConstant<SixtyFive, long>
    {
        long IConstant<SixtyFive, long>.Value => 65;
    }

    public readonly struct SixtySix : IConstant<SixtySix, long>
    {
        long IConstant<SixtySix, long>.Value => 66;
    }

    public readonly struct SixtySeven : IConstant<SixtySeven, long>
    {
        long IConstant<SixtySeven, long>.Value => 67;
    }

    public readonly struct SixtyEight : IConstant<SixtyEight, long>
    {
        long IConstant<SixtyEight, long>.Value => 68;
    }

    public readonly struct SixtyNine : IConstant<SixtyNine, long>
    {
        long IConstant<SixtyNine, long>.Value => 69;
    }

    public readonly struct Seventy : IConstant<Seventy, long>
    {
        long IConstant<Seventy, long>.Value => 70;
    }

    public readonly struct SeventyOne : IConstant<SeventyOne, long>
    {
        long IConstant<SeventyOne, long>.Value => 71;
    }

    public readonly struct SeventyTwo : IConstant<SeventyTwo, long>
    {
        long IConstant<SeventyTwo, long>.Value => 72;
    }

    public readonly struct SeventyThree : IConstant<SeventyThree, long>
    {
        long IConstant<SeventyThree, long>.Value => 73;
    }

    public readonly struct SeventyFour : IConstant<SeventyFour, long>
    {
        long IConstant<SeventyFour, long>.Value => 74;
    }

    public readonly struct SeventyFive : IConstant<SeventyFive, long>
    {
        long IConstant<SeventyFive, long>.Value => 75;
    }

    public readonly struct SeventySix : IConstant<SeventySix, long>
    {
        long IConstant<SeventySix, long>.Value => 76;
    }

    public readonly struct SeventySeven : IConstant<SeventySeven, long>
    {
        long IConstant<SeventySeven, long>.Value => 77;
    }

    public readonly struct SeventyEight : IConstant<SeventyEight, long>
    {
        long IConstant<SeventyEight, long>.Value => 78;
    }

    public readonly struct SeventyNine : IConstant<SeventyNine, long>
    {
        long IConstant<SeventyNine, long>.Value => 79;
    }

    public readonly struct Eighty : IConstant<Eighty, long>
    {
        long IConstant<Eighty, long>.Value => 80;
    }

    public readonly struct EightyOne : IConstant<EightyOne, long>
    {
        long IConstant<EightyOne, long>.Value => 81;
    }

    public readonly struct EightyTwo : IConstant<EightyTwo, long>
    {
        long IConstant<EightyTwo, long>.Value => 82;
    }

    public readonly struct EightyThree : IConstant<EightyThree, long>
    {
        long IConstant<EightyThree, long>.Value => 83;
    }

    public readonly struct EightyFour : IConstant<EightyFour, long>
    {
        long IConstant<EightyFour, long>.Value => 84;
    }

    public readonly struct EightyFive : IConstant<EightyFive, long>
    {
        long IConstant<EightyFive, long>.Value => 85;
    }

    public readonly struct EightySix : IConstant<EightySix, long>
    {
        long IConstant<EightySix, long>.Value => 86;
    }

    public readonly struct EightySeven : IConstant<EightySeven, long>
    {
        long IConstant<EightySeven, long>.Value => 87;
    }

    public readonly struct EightyEight : IConstant<EightyEight, long>
    {
        long IConstant<EightyEight, long>.Value => 88;
    }

    public readonly struct EightyNine : IConstant<EightyNine, long>
    {
        long IConstant<EightyNine, long>.Value => 89;
    }

    public readonly struct Ninety : IConstant<Ninety, long>
    {
        long IConstant<Ninety, long>.Value => 90;
    }

    public readonly struct NinetyOne : IConstant<NinetyOne, long>
    {
        long IConstant<NinetyOne, long>.Value => 91;
    }

    public readonly struct NinetyTwo : IConstant<NinetyTwo, long>
    {
        long IConstant<NinetyTwo, long>.Value => 92;
    }

    public readonly struct NinetyThree : IConstant<NinetyThree, long>
    {
        long IConstant<NinetyThree, long>.Value => 93;
    }

    public readonly struct NinetyFour : IConstant<NinetyFour, long>
    {
        long IConstant<NinetyFour, long>.Value => 94;
    }

    public readonly struct NinetyFive : IConstant<NinetyFive, long>
    {
        long IConstant<NinetyFive, long>.Value => 95;
    }

    public readonly struct NinetySix : IConstant<NinetySix, long>
    {
        long IConstant<NinetySix, long>.Value => 96;
    }

    public readonly struct NinetySeven : IConstant<NinetySeven, long>
    {
        long IConstant<NinetySeven, long>.Value => 97;
    }

    public readonly struct NinetyEight : IConstant<NinetyEight, long>
    {
        long IConstant<NinetyEight, long>.Value => 98;
    }

    public readonly struct NinetyNine : IConstant<NinetyNine, long>
    {
        long IConstant<NinetyNine, long>.Value => 99;
    }

    public readonly struct OneHundred : IConstant<OneHundred, long>
    {
        long IConstant<OneHundred, long>.Value => 100;
    }

    public readonly struct MaxInt : IConstant<MaxInt, long>
    {
        long IConstant<MaxInt, long>.Value => int.MaxValue;
    }

    public readonly struct MaxLong : IConstant<MaxLong, long>
    {
        long IConstant<MaxLong, long>.Value => long.MaxValue;
    }

    public readonly struct MinInt : IConstant<MinInt, long>
    {
        long IConstant<MinInt, long>.Value => int.MinValue;
    }

    public readonly struct MinLong : IConstant<MinLong, long>
    {
        long IConstant<MinLong, long>.Value => long.MinValue;
    }

    public readonly struct MaxByte : IConstant<MaxByte, long>
    {
        long IConstant<MaxByte, long>.Value => byte.MaxValue;
    }

    public readonly struct MaxSByte : IConstant<MaxSByte, long>
    {
        long IConstant<MaxSByte, long>.Value => sbyte.MaxValue;
    }

    public readonly struct MaxShort : IConstant<MaxShort, long>
    {
        long IConstant<MaxShort, long>.Value => short.MaxValue;
    }

    public readonly struct MaxUShort : IConstant<MaxUShort, long>
    {
        long IConstant<MaxUShort, long>.Value => ushort.MaxValue;
    }

    public readonly struct MaxUInt : IConstant<MaxUInt, long>
    {
        long IConstant<MaxUInt, long>.Value => uint.MaxValue;
    }

    public readonly struct MinByte : IConstant<MinByte, long>
    {
        long IConstant<MinByte, long>.Value => byte.MinValue;
    }

    public readonly struct MinSByte : IConstant<MinSByte, long>
    {
        long IConstant<MinSByte, long>.Value => sbyte.MinValue;
    }

    public readonly struct MinShort : IConstant<MinShort, long>
    {
        long IConstant<MinShort, long>.Value => short.MinValue;
    }

    public readonly struct MinUShort : IConstant<MinUShort, long>
    {
        long IConstant<MinUShort, long>.Value => ushort.MinValue;
    }

    public readonly struct MinUInt : IConstant<MinUInt, long>
    {
        long IConstant<MinUInt, long>.Value => uint.MinValue;
    }
}
