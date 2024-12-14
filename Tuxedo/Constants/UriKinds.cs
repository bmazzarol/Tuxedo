namespace Tuxedo.Constants;

/// <summary>
/// A constant representing the absolute URI kind
/// </summary>
public readonly struct AbsoluteUriKind : IConstant<AbsoluteUriKind, UriKind>
{
    /// <inheritdoc />
    public static UriKind Value => UriKind.Absolute;
}

/// <summary>
/// A constant representing the relative URI kind
/// </summary>
public readonly struct RelativeUriKind : IConstant<RelativeUriKind, UriKind>
{
    /// <inheritdoc />
    public static UriKind Value => UriKind.Relative;
}

/// <summary>
/// A constant representing the relative or absolute URI kind
/// </summary>
public readonly struct AbsoluteOrRelativeUriKind : IConstant<AbsoluteOrRelativeUriKind, UriKind>
{
    /// <inheritdoc />
    public static UriKind Value => UriKind.RelativeOrAbsolute;
}
