using System.Security;

namespace Tuxedo.SourceGenerator.Extensions;

internal static class StringExtensions
{
    public static string RemoveNamespace(this string value)
    {
        var parts = value.Split('.');
        return parts[parts.Length - 1];
    }

    public static string RemoveGenerics(this string value)
    {
        var parts = value.Split('<');
        return parts[0];
    }

    public static string? EscapeXml(this string? value)
    {
        return value == null ? null : SecurityElement.Escape(value);
    }

    public static string StripExpressionParts(this string value)
    {
        if (
            value.StartsWith("nameof(", StringComparison.Ordinal)
            && value.EndsWith(")", StringComparison.Ordinal)
        )
        {
            return value.Substring(7, value.Length - 8);
        }
        return value.Replace("\"", string.Empty);
    }

    public static string? PrependIfNotNull(this string? value, string prepend)
    {
        return value == null ? value : $"{prepend}{value}";
    }

    public static string? RenderIfNotNull(this string? value, Func<string, string> render)
    {
        return value == null ? null : render(value);
    }

    public static string? LowercaseFirst(this string? value)
    {
        return value == null ? null : char.ToLowerInvariant(value[0]) + value.Substring(1);
    }

    public static string? UppercaseFirst(this string? value)
    {
        return value == null ? null : char.ToUpperInvariant(value[0]) + value.Substring(1);
    }
}
