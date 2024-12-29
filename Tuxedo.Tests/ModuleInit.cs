using System.Runtime.CompilerServices;

namespace Tuxedo.Tests;

public static class ModuleInit
{
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Initialize();
}
