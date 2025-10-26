using System.Collections.Immutable;
using System.Text;
using Cutout;

namespace Tuxedo.SourceGenerator;

internal static partial class TemplateProvider
{
    [FileTemplate]
    internal static partial void WriteRefinementService(
        this StringBuilder builder,
        ImmutableArray<RefinedTypeDetails> refinedTypeDetails
    );

    [FileTemplate]
    internal static partial void WriteNamespaceParts(
        this StringBuilder builder,
        RefinedTypeDetails model
    );

    [FileTemplate]
    internal static partial void WriteTypeNameParts(
        this StringBuilder builder,
        RefinedTypeDetails model
    );

    [FileTemplate]
    internal static partial void WriteTypePropertyParts(
        this StringBuilder builder,
        string name,
        string? typeName,
        RefinedTypeDetails model,
        bool addImplicitOperator
    );

    [FileTemplate]
    internal static partial void WriteParseMethod(
        this StringBuilder builder,
        RefinedTypeDetails model
    );

    [FileTemplate]
    internal static partial void WriteTryParseMethod(
        this StringBuilder builder,
        RefinedTypeDetails model
    );

    [FileTemplate]
    internal static partial void WriteEqualityMembers(
        this StringBuilder builder,
        RefinedTypeDetails model
    );

    [FileTemplate]
    internal static partial void WriteFormattingMembers(
        this StringBuilder builder,
        RefinedTypeDetails model
    );

    [FileTemplate]
    internal static partial void WriteSingleRefinedType(
        this StringBuilder builder,
        RefinedTypeDetails model
    );

    [FileTemplate]
    internal static partial void WriteMultiRefinedType(
        this StringBuilder builder,
        RefinedTypeDetails model
    );
}
