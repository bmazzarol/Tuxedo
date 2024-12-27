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
}
