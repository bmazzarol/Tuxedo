#pragma warning disable S3011

using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Tuxedo.Extensions;

internal static class ExceptionExtensions
{
    internal static Exception WithStackTrace(this Exception target, StackTrace stack) =>
        SetStackTraceFunction(target, stack);

    private static readonly Func<Exception, StackTrace, Exception> SetStackTraceFunction = new Func<
        Func<Exception, StackTrace, Exception>
    >(() =>
    {
        var target = Expression.Parameter(typeof(Exception));
        var stack = Expression.Parameter(typeof(StackTrace));
        var traceFormatType = typeof(StackTrace).GetNestedType(
            "TraceFormat",
            BindingFlags.NonPublic
        );
        var toString = typeof(StackTrace).GetMethod(
            "ToString",
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            [traceFormatType],
            null
        );
        var normalTraceFormat = Enum.GetValues(traceFormatType).GetValue(0);
        var stackTraceString = Expression.Call(
            stack,
            toString,
            Expression.Constant(normalTraceFormat, traceFormatType)
        );
        var stackTraceStringField = typeof(Exception).GetField(
            "_remoteStackTraceString",
            BindingFlags.NonPublic | BindingFlags.Instance
        );
        var assign = Expression.Assign(
            Expression.Field(target, stackTraceStringField),
            stackTraceString
        );
        return Expression
            .Lambda<Func<Exception, StackTrace, Exception>>(
                Expression.Block(assign, target),
                target,
                stack
            )
            .Compile();
    })();
}
