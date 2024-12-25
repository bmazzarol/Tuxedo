using System.Diagnostics.CodeAnalysis;

namespace Tuxedo.Tests.CustomTypes;

using Template = Raw<string>.IRefinedType<ItemNumberString, ItemNumberString>;

/// <summary>
/// Example of a refined type that is also the refinement
/// </summary>
public readonly struct ItemNumberString : Template, IRefinement<ItemNumberString, string>
{
    public string Value { get; }

    private ItemNumberString(string value)
    {
        Value = value;
    }

    public static implicit operator string(ItemNumberString refinedValue)
    {
        return refinedValue.Value;
    }

    public static explicit operator ItemNumberString(string value)
    {
        return Parse(value);
    }

    public static ItemNumberString Parse(string value)
    {
        return Template.ParseInternal(value);
    }

    public static bool TryParse(
        string value,
        out ItemNumberString refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        return Template.TryParseInternal(value, out refined, out failureMessage);
    }

    static ItemNumberString IRefinement<ItemNumberString, string>.Value { get; }

    public bool IsRefined(string value)
    {
        return value.Length == 4
            && char.IsDigit(value[0])
            && char.IsDigit(value[1])
            && char.IsDigit(value[2])
            && char.IsDigit(value[3]);
    }

    public string BuildFailureMessage(string value)
    {
        return $"'{value}' is not a valid item number.";
    }
}
