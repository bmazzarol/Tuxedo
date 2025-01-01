using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator;

public sealed partial class RefinementSourceGenerator
{
    private static string RenderSingleRefinedType(RefinedTypeDetails model)
    {
        return $$"""
                 {{RenderNamespaceParts(model)}}

                 {{RenderTypeNameParts(model)}}
                 {
                     {{RenderTypePropertyParts(
                         "Value",
                         model.RawType,
                         model,
                         addImplicitOperator: true)}}
                     
                     private {{model.RefinedType}}({{model.RawType}} value)
                     {
                         _value = value;
                     }
                 
                     {{RenderParseMethod(model)}}
                     
                     {{RenderTryParseMethod(model)}}
                     
                     {{RenderEqualityMembers(model)}}
                 }
                 """;
    }

    private static string RenderNamespaceParts(RefinedTypeDetails model)
    {
        return $"""
            // <auto-generated/>
            #nullable enable

            using System;
            using System.Diagnostics.CodeAnalysis;

            namespace {model.Namespace};
            """;
    }

    private static string RenderTypeNameParts(RefinedTypeDetails model)
    {
        return $"""
            /// <summary>
            /// A refined {model.RawType.EscapeXml()} based on the {model.Predicate.EscapeXml()} refinement predicate{model.AlternativeType.RenderIfNotNull(
                x => $" which produces an alternative {x.EscapeXml()} value"
            )}
            /// </summary>
            [Tuxedo.RefinedType]
            {model.AccessModifier} readonly partial struct {model.RefinedType}{model.Generics} : IEquatable<{model.RefinedType}{model.Generics}>{model.GenericConstraints.PrependIfNotNull(
                "\n\t"
            )}
            """;
    }

    private static string RenderTypePropertyParts(
        string name,
        string? typeName,
        RefinedTypeDetails model,
        bool addImplicitOperator
    )
    {
        var result = $"""
            private readonly {typeName}? _{name.LowercaseFirst()};
               
                /// <summary>
                /// The underlying {typeName.EscapeXml()}
                /// </summary>
                public {typeName} {name} => _{name.LowercaseFirst()} ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a {model.RefinedType}");
            """;

        if (addImplicitOperator)
        {
            result += $$"""


                    /// <summary>
                    /// Implicit conversion from the {{model.RefinedTypeXmlSafeName}} to a {{typeName.EscapeXml()}}
                    /// </summary>
                    /// <param name="this">the {{model.RefinedTypeXmlSafeName}}</param>
                    /// <returns>underlying {{typeName.EscapeXml()}}</returns>
                    public static implicit operator {{typeName}}({{model.RefinedType}}{{model.Generics}} @this)
                    {
                        return @this.{{name}};
                    }
                """;
        }

        return result;
    }

    private static string RenderParseMethod(RefinedTypeDetails model)
    {
        return $$"""
            /// <summary>
                /// Explicit conversion from a {{model.RawType.EscapeXml()}} to a {{model.RefinedTypeXmlSafeName}}
                /// </summary>
                /// <param name="value">raw {{model.RawType.EscapeXml()}}</param>
                /// <returns>refined {{model.RefinedTypeXmlSafeName}}</returns>
                /// <exception cref="ArgumentOutOfRangeException">if the {{model.Predicate.EscapeXml()}} refinement fails</exception>
                public static explicit operator {{model.RefinedType}}{{model.Generics}}({{model.RawType}} value)
                {
                    return Parse(value);
                }
                
                /// <summary>
                /// Refines the {{model.RawType.EscapeXml()}} or throws
                /// </summary>
                /// <param name="value">raw {{model.RawType.EscapeXml()}}</param>
                /// <returns>refined {{model.RefinedTypeXmlSafeName}}</returns>
                /// <exception cref="ArgumentOutOfRangeException">if the {{model.Predicate.EscapeXml()}} refinement fails</exception>
                public static {{model.RefinedType}}{{model.Generics}} Parse({{model.RawType}} value)
                {
                    return TryParse(value, out var result, out var failureMessage) ? result : throw new ArgumentOutOfRangeException(nameof(value), value, failureMessage);
                }
            """;
    }

    private static string RenderTryParseMethod(RefinedTypeDetails model)
    {
        return $$"""
            /// <summary>
                /// Try and refine the {{model.RawType.EscapeXml()}} against the {{model.Predicate.EscapeXml()}} refinement{{model.AlternativeType.RenderIfNotNull(x => $" producing a {x.EscapeXml()}")}}
                /// </summary>
                /// <param name="value">raw {{model.RawType.EscapeXml()}}</param>
                /// <param name="refined">refined {{model.RefinedTypeXmlSafeName}} when true</param>
                /// <param name="failureMessage">error message when false</param>
                /// <returns>true if refined, false otherwise</returns>
                public static bool TryParse(
                    {{model.RawType}} value,
                    out {{model.RefinedType}}{{model.Generics}} refined,
                    [NotNullWhen(false)] out string? failureMessage
                )
                {
                    if ({{model.Predicate}}{{model.Generics}}(value{{model.AlternativeType.RenderIfNotNull(
                _ => ", out var altValue"
            )}}))
                    {
                        refined = new {{model.RefinedType}}{{model.Generics}}(value{{model.AlternativeType.RenderIfNotNull(_ => ", altValue")}});
                        failureMessage = null;
                        return true;
                    }
                    
                    refined = default!;
                    failureMessage = ${{model.FailureMessage}};
                    return false;
                }
            """;
    }

    private static string RenderEqualityMembers(RefinedTypeDetails model)
    {
        var hasAltValue = model.AlternativeType is not null;
        var equals = hasAltValue
            ? "Nullable.Equals(_value, other._value) && Nullable.Equals(_altValue, other._altValue)"
            : "Nullable.Equals(_value, other._value)";
        var hashCode = hasAltValue
            ? "HashCode.Combine(_value, _altValue)"
            : "HashCode.Combine(_value)";
        return $$"""
            // <inheritdoc />
                public bool Equals({{model.RefinedType}}{{model.Generics}} other)
                {
                    return {{equals}};
                }
                
                /// <inheritdoc />
                public override bool Equals(object? obj)
                {
                    return obj is {{model.RefinedType}}{{model.Generics}} other && Equals(other);
                }
                
                /// <inheritdoc />
                public override int GetHashCode()
                {
                    return {{hashCode}};
                }
            """;
    }
}
