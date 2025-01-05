using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Tuxedo.SourceGenerator.Extensions;

namespace Tuxedo.SourceGenerator.Analysers;

public sealed partial class InvalidConstAssignmentAnalyser
{
    private Lazy<RefinementService?>? _refinementService;

    /// <summary>
    /// Provides access to the runtime refinement predicates for refined types
    /// </summary>
    private sealed class RefinementService(Type runtimeRefinementServiceType)
    {
        private readonly ConcurrentDictionary<string, Func<object, string?>?> _refinementDelegates =
            new(StringComparer.Ordinal);

        public static RefinementService? Build(Compilation compilation, CancellationToken token)
        {
            using var ms = new MemoryStream();
            var result = compilation.Emit(ms, cancellationToken: token);

            if (!result.Success)
            {
                return null;
            }

            ms.Seek(0, SeekOrigin.Begin);
#pragma warning disable RS1035 // we need to load it here, as we want to run code against the current state of the compilation
            var assembly = Assembly.Load(ms.ToArray());
#pragma warning restore RS1035
            var type = assembly.GetType("Tuxedo.RefinementService");

            return new RefinementService(type);
        }

        public string? TestAgainst(string refinedTypeName, object value)
        {
            var refinementDelegate = _refinementDelegates.GetOrAdd(
                refinedTypeName,
                BuildRefinementDelegate
            );

            return refinementDelegate?.Invoke(value);
        }

        private Func<object, string?>? BuildRefinementDelegate(string fn)
        {
            var methodInfo = runtimeRefinementServiceType.GetMethod(
                $"TestAgainst{fn.RemoveNamespace()}",
#pragma warning disable S3011
                BindingFlags.NonPublic | BindingFlags.Static
#pragma warning restore S3011
            );

            if (methodInfo == null)
            {
                return null;
            }

            return (Func<object, string?>?)methodInfo.CreateDelegate(typeof(Func<object, string?>));
        }
    }
}
