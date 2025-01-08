#pragma warning disable MA0051

using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator;

public sealed partial class RefinementSourceGenerator
{
    private static string RenderMultiRefinedType(RefinedTypeDetails model)
    {
        return $$"""
                 {{RenderNamespaceParts(model)}}

                 {{RenderTypeNameParts(model)}}
                 {
                     {{RenderTypePropertyParts(
                         "Value",
                         model.RawType,
                         model,
                         addImplicitOperator: !string.Equals(model.RawType, model.AlternativeType, StringComparison.Ordinal))}}
                         
                     {{RenderTypePropertyParts(
                         "AltValue",
                         model.AlternativeType,
                         model,
                         addImplicitOperator: true)}}

                     private {{model.RefinedType}}({{model.RawType}} value, {{model.AlternativeType}} altValue)
                     {
                         _value = value;
                         _altValue = altValue;
                     }

                     {{RenderParseMethod(model)}}
                 
                     {{RenderTryParseMethod(model)}}
                     
                     {{RenderEqualityMembers(model)}}
                     
                     {{RenderFormattingMembers(model)}}
                     
                     /// <summary>
                     /// Standard deconstruction to the underlying values
                     /// </summary>
                     /// <param name="value">raw {{model.RawType.EscapeXml()}}</param>
                     /// <param name="altValue">alternative {{model.AlternativeType.EscapeXml()}}</param>
                     public void Deconstruct(out {{model.RawType}} value, out {{model.AlternativeType}} altValue)
                     {
                          value = Value;
                          altValue = AltValue;
                     }
                 }
                 """;
    }
}
