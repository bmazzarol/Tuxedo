using Tuxedo.Tests.Extensions;

namespace Tuxedo.Tests;

/// <summary>
/// Represents a string that is also a valid Guid
/// </summary>
public readonly partial struct GuidString
{
    // custom fields and methods can be added to the refined type
    public byte[] Bytes => AltValue.ToByteArray();

    public bool IsEmpty => AltValue == System.Guid.Empty;

    [Refinement("The value must be a valid GUID, but was '{value}'")]
    private static bool Guid(string value, out Guid guid) => System.Guid.TryParse(value, out guid);
}

public sealed class GuidStringTests
{
    [Fact(DisplayName = "A string can be refined into a GUID")]
    public static void Case1()
    {
        var refined = (GuidString)"6192C5ED-505C-4558-B87C-CA6E7D612B31";
        Assert.Equal("6192C5ED-505C-4558-B87C-CA6E7D612B31", refined.Value);
        Assert.Equal(new Guid("6192C5ED-505C-4558-B87C-CA6E7D612B31"), refined.AltValue);
        Assert.NotEmpty(refined.Bytes);
        Assert.False(refined.IsEmpty);

        var (str, guid) = refined;
        Assert.Equal("6192C5ED-505C-4558-B87C-CA6E7D612B31", str);
        Assert.Equal(new Guid("6192C5ED-505C-4558-B87C-CA6E7D612B31"), guid);

        Assert.True(
            GuidString.TryParse(
                "6192C5ED-505C-4558-B87C-CA6E7D612B31",
                out var refined2,
                out var failureMessage
            )
        );
        Assert.Equal(refined, refined2);
        Assert.Null(failureMessage);
    }

    [Fact(DisplayName = "A string cannot be refined to a GUID")]
    public static void Case2()
    {
        const string value = "not a guid";
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => (GuidString)value);
        Assert.StartsWith("The value must be a valid GUID, but was 'not a guid'", ex.Message);
        Assert.False(GuidString.TryParse(value, out _, out _));
    }

    [Fact(DisplayName = "GuidString refinement snapshot is correct")]
    public Task Case3()
    {
        return """
            [Refinement("The value must be a valid GUID, but was '{value}'")]
            private static bool Guid(string value, out Guid guid) => System.Guid.TryParse(value, out guid);
            """.VerifyRefinement();
    }
}
