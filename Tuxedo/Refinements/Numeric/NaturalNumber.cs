using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Tuxedo;

/// <summary>
/// The natural numbers to use in refinements
/// </summary>
[ExcludeFromCodeCoverage]
public static class NaturalNumber
{
    public readonly struct Zero : IConstant<Zero, long>
    {
        public long Value => 0;
    }

    public readonly struct One : IConstant<One, long>
    {
        public long Value => 1;
    }

    public readonly struct Two : IConstant<Two, long>
    {
        public long Value => 2;
    }

    public readonly struct Three : IConstant<Three, long>
    {
        public long Value => 3;
    }

    public readonly struct Four : IConstant<Four, long>
    {
        public long Value => 4;
    }

    public readonly struct Five : IConstant<Five, long>
    {
        public long Value => 5;
    }

    public readonly struct Six : IConstant<Six, long>
    {
        public long Value => 6;
    }

    public readonly struct Seven : IConstant<Seven, long>
    {
        public long Value => 7;
    }

    public readonly struct Eight : IConstant<Eight, long>
    {
        public long Value => 8;
    }

    public readonly struct Nine : IConstant<Nine, long>
    {
        public long Value => 9;
    }

    public readonly struct Ten : IConstant<Ten, long>
    {
        public long Value => 10;
    }

    public readonly struct Eleven : IConstant<Eleven, long>
    {
        public long Value => 11;
    }

    public readonly struct Twelve : IConstant<Twelve, long>
    {
        public long Value => 12;
    }

    public readonly struct Thirteen : IConstant<Thirteen, long>
    {
        public long Value => 13;
    }

    public readonly struct Fourteen : IConstant<Fourteen, long>
    {
        public long Value => 14;
    }

    public readonly struct Fifteen : IConstant<Fifteen, long>
    {
        public long Value => 15;
    }

    public readonly struct Sixteen : IConstant<Sixteen, long>
    {
        public long Value => 16;
    }

    public readonly struct Seventeen : IConstant<Seventeen, long>
    {
        public long Value => 17;
    }

    public readonly struct Eighteen : IConstant<Eighteen, long>
    {
        public long Value => 18;
    }

    public readonly struct Nineteen : IConstant<Nineteen, long>
    {
        public long Value => 19;
    }

    public readonly struct Twenty : IConstant<Twenty, long>
    {
        public long Value => 20;
    }

    public readonly struct TwentyOne : IConstant<TwentyOne, long>
    {
        public long Value => 21;
    }

    public readonly struct TwentyTwo : IConstant<TwentyTwo, long>
    {
        public long Value => 22;
    }

    public readonly struct TwentyThree : IConstant<TwentyThree, long>
    {
        public long Value => 23;
    }

    public readonly struct TwentyFour : IConstant<TwentyFour, long>
    {
        public long Value => 24;
    }

    public readonly struct TwentyFive : IConstant<TwentyFive, long>
    {
        public long Value => 25;
    }

    public readonly struct TwentySix : IConstant<TwentySix, long>
    {
        public long Value => 26;
    }

    public readonly struct TwentySeven : IConstant<TwentySeven, long>
    {
        public long Value => 27;
    }

    public readonly struct TwentyEight : IConstant<TwentyEight, long>
    {
        public long Value => 28;
    }

    public readonly struct TwentyNine : IConstant<TwentyNine, long>
    {
        public long Value => 29;
    }

    public readonly struct Thirty : IConstant<Thirty, long>
    {
        public long Value => 30;
    }

    public readonly struct ThirtyOne : IConstant<ThirtyOne, long>
    {
        public long Value => 31;
    }

    public readonly struct ThirtyTwo : IConstant<ThirtyTwo, long>
    {
        public long Value => 32;
    }

    public readonly struct ThirtyThree : IConstant<ThirtyThree, long>
    {
        public long Value => 33;
    }

    public readonly struct ThirtyFour : IConstant<ThirtyFour, long>
    {
        public long Value => 34;
    }

    public readonly struct ThirtyFive : IConstant<ThirtyFive, long>
    {
        public long Value => 35;
    }

    public readonly struct ThirtySix : IConstant<ThirtySix, long>
    {
        public long Value => 36;
    }

    public readonly struct ThirtySeven : IConstant<ThirtySeven, long>
    {
        public long Value => 37;
    }

    public readonly struct ThirtyEight : IConstant<ThirtyEight, long>
    {
        public long Value => 38;
    }

    public readonly struct ThirtyNine : IConstant<ThirtyNine, long>
    {
        public long Value => 39;
    }

    public readonly struct Forty : IConstant<Forty, long>
    {
        public long Value => 40;
    }

    public readonly struct FortyOne : IConstant<FortyOne, long>
    {
        public long Value => 41;
    }

    public readonly struct FortyTwo : IConstant<FortyTwo, long>
    {
        public long Value => 42;
    }

    public readonly struct FortyThree : IConstant<FortyThree, long>
    {
        public long Value => 43;
    }

    public readonly struct FortyFour : IConstant<FortyFour, long>
    {
        public long Value => 44;
    }

    public readonly struct FortyFive : IConstant<FortyFive, long>
    {
        public long Value => 45;
    }

    public readonly struct FortySix : IConstant<FortySix, long>
    {
        public long Value => 46;
    }

    public readonly struct FortySeven : IConstant<FortySeven, long>
    {
        public long Value => 47;
    }

    public readonly struct FortyEight : IConstant<FortyEight, long>
    {
        public long Value => 48;
    }

    public readonly struct FortyNine : IConstant<FortyNine, long>
    {
        public long Value => 49;
    }

    public readonly struct Fifty : IConstant<Fifty, long>
    {
        public long Value => 50;
    }

    public readonly struct FiftyOne : IConstant<FiftyOne, long>
    {
        public long Value => 51;
    }

    public readonly struct FiftyTwo : IConstant<FiftyTwo, long>
    {
        public long Value => 52;
    }

    public readonly struct FiftyThree : IConstant<FiftyThree, long>
    {
        public long Value => 53;
    }

    public readonly struct FiftyFour : IConstant<FiftyFour, long>
    {
        public long Value => 54;
    }

    public readonly struct FiftyFive : IConstant<FiftyFive, long>
    {
        public long Value => 55;
    }

    public readonly struct FiftySix : IConstant<FiftySix, long>
    {
        public long Value => 56;
    }

    public readonly struct FiftySeven : IConstant<FiftySeven, long>
    {
        public long Value => 57;
    }

    public readonly struct FiftyEight : IConstant<FiftyEight, long>
    {
        public long Value => 58;
    }

    public readonly struct FiftyNine : IConstant<FiftyNine, long>
    {
        public long Value => 59;
    }

    public readonly struct Sixty : IConstant<Sixty, long>
    {
        public long Value => 60;
    }

    public readonly struct SixtyOne : IConstant<SixtyOne, long>
    {
        public long Value => 61;
    }

    public readonly struct SixtyTwo : IConstant<SixtyTwo, long>
    {
        public long Value => 62;
    }

    public readonly struct SixtyThree : IConstant<SixtyThree, long>
    {
        public long Value => 63;
    }

    public readonly struct SixtyFour : IConstant<SixtyFour, long>
    {
        public long Value => 64;
    }

    public readonly struct SixtyFive : IConstant<SixtyFive, long>
    {
        public long Value => 65;
    }

    public readonly struct SixtySix : IConstant<SixtySix, long>
    {
        public long Value => 66;
    }

    public readonly struct SixtySeven : IConstant<SixtySeven, long>
    {
        public long Value => 67;
    }

    public readonly struct SixtyEight : IConstant<SixtyEight, long>
    {
        public long Value => 68;
    }

    public readonly struct SixtyNine : IConstant<SixtyNine, long>
    {
        public long Value => 69;
    }

    public readonly struct Seventy : IConstant<Seventy, long>
    {
        public long Value => 70;
    }

    public readonly struct SeventyOne : IConstant<SeventyOne, long>
    {
        public long Value => 71;
    }

    public readonly struct SeventyTwo : IConstant<SeventyTwo, long>
    {
        public long Value => 72;
    }

    public readonly struct SeventyThree : IConstant<SeventyThree, long>
    {
        public long Value => 73;
    }

    public readonly struct SeventyFour : IConstant<SeventyFour, long>
    {
        public long Value => 74;
    }

    public readonly struct SeventyFive : IConstant<SeventyFive, long>
    {
        public long Value => 75;
    }

    public readonly struct SeventySix : IConstant<SeventySix, long>
    {
        public long Value => 76;
    }

    public readonly struct SeventySeven : IConstant<SeventySeven, long>
    {
        public long Value => 77;
    }

    public readonly struct SeventyEight : IConstant<SeventyEight, long>
    {
        public long Value => 78;
    }

    public readonly struct SeventyNine : IConstant<SeventyNine, long>
    {
        public long Value => 79;
    }

    public readonly struct Eighty : IConstant<Eighty, long>
    {
        public long Value => 80;
    }

    public readonly struct EightyOne : IConstant<EightyOne, long>
    {
        public long Value => 81;
    }

    public readonly struct EightyTwo : IConstant<EightyTwo, long>
    {
        public long Value => 82;
    }

    public readonly struct EightyThree : IConstant<EightyThree, long>
    {
        public long Value => 83;
    }

    public readonly struct EightyFour : IConstant<EightyFour, long>
    {
        public long Value => 84;
    }

    public readonly struct EightyFive : IConstant<EightyFive, long>
    {
        public long Value => 85;
    }

    public readonly struct EightySix : IConstant<EightySix, long>
    {
        public long Value => 86;
    }

    public readonly struct EightySeven : IConstant<EightySeven, long>
    {
        public long Value => 87;
    }

    public readonly struct EightyEight : IConstant<EightyEight, long>
    {
        public long Value => 88;
    }

    public readonly struct EightyNine : IConstant<EightyNine, long>
    {
        public long Value => 89;
    }

    public readonly struct Ninety : IConstant<Ninety, long>
    {
        public long Value => 90;
    }

    public readonly struct NinetyOne : IConstant<NinetyOne, long>
    {
        public long Value => 91;
    }

    public readonly struct NinetyTwo : IConstant<NinetyTwo, long>
    {
        public long Value => 92;
    }

    public readonly struct NinetyThree : IConstant<NinetyThree, long>
    {
        public long Value => 93;
    }

    public readonly struct NinetyFour : IConstant<NinetyFour, long>
    {
        public long Value => 94;
    }

    public readonly struct NinetyFive : IConstant<NinetyFive, long>
    {
        public long Value => 95;
    }

    public readonly struct NinetySix : IConstant<NinetySix, long>
    {
        public long Value => 96;
    }

    public readonly struct NinetySeven : IConstant<NinetySeven, long>
    {
        public long Value => 97;
    }

    public readonly struct NinetyEight : IConstant<NinetyEight, long>
    {
        public long Value => 98;
    }

    public readonly struct NinetyNine : IConstant<NinetyNine, long>
    {
        public long Value => 99;
    }

    public readonly struct OneHundred : IConstant<OneHundred, long>
    {
        public long Value => 100;
    }
}
