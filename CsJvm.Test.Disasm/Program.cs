using CsJvm.Abstractions.Disasm;
using CsJvm.Abstractions.Loader;
using CsJvm.Disasm;
using CsJvm.Loader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// use java runtime library from Java 8
const string jarName = "rt.jar";
const string className = "java/util/stream/Streams$IntStreamBuilderImpl";
const string methodName = "accept:(I)V";

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLogging(x => x.AddConsole())
                .AddTransient<IClassFileLoader, ClassFileLoader>()
                .AddTransient<IJavaClassLoader, JavaClassLoader>()
                .AddTransient<IJarLoader, JarLoader>()
                .AddTransient<IDisasmDescriptionProvider, DisasmDescriptionProvider>()
                .AddTransient<IDisasm, Disassembler>();
    })
    .Build();

using var loader = host.Services.GetRequiredService<IJarLoader>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();
var disasm = host.Services.GetRequiredService<IDisasm>();

if (!loader.TryOpen(jarName) || loader.JAR == null)
{
    logger.LogError("Cannot open {jarName}!", jarName);
    return;
}

if (!loader.TryGetClass(className, out var javaClass) || javaClass == null)
{
    logger.LogError("Cannot find class '{className}'!", className);
    return;
}

if (!javaClass.Methods.TryGetValue(methodName, out var method) || method == null)
{
    logger.LogError("Cannot find method '{methodName}'!", methodName);
    return;
}

var asm = disasm.GetDisasmString(method, javaClass.ConstantPool);
logger.LogInformation("{asm}", asm);

await host.RunAsync();
