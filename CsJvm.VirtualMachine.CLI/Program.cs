using CsJvm.Abstractions.Disasm;
using CsJvm.Abstractions.Instructions;
using CsJvm.Abstractions.Loader;
using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Disasm;
using CsJvm.Loader;
using CsJvm.VirtualMachine;
using CsJvm.VirtualMachine.Heap;
using CsJvm.VirtualMachine.Instructions.Interpreter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLogging(x => x.AddConsole())
                // loaders
                .AddTransient<IClassFileLoader, ClassFileLoader>()
                .AddTransient<IJavaClassLoader, JavaClassLoader>()
                .AddTransient<IJarLoader, JarLoader>()

                // disassembler
                .AddTransient<IDisasmDescriptionProvider, DisasmDescriptionProvider>()
                .AddTransient<IDisasm, Disassembler>()

                // opcodes decoder
                .AddSingleton<IOpcodes, JvmInterpreter>()
                .AddSingleton<IOpcodesDecoder, OpcodesDecoder>()

                // native methods implementations
                .AddSingleton<INativeMethodsProvider, NativeMethodsProvider>()
                .AddSingleton<CsJvm.VirtualMachine.Natives.Java.Lang.Class>()
                .AddSingleton<CsJvm.VirtualMachine.Natives.Java.Lang.Object>()
                .AddSingleton<CsJvm.VirtualMachine.Natives.Java.Lang.String>()
                .AddSingleton<CsJvm.VirtualMachine.Natives.Java.Lang.System>()

                // java machine
                .AddSingleton<IJavaHeap, JavaHeap>()
                .AddSingleton<IJavaExecutable, JavaExecutable>()
                .AddSingleton<IJavaRuntime, JavaRuntime>()
                .AddSingleton<IJavaMachine, JavaMachine>();
    })
    .ConfigureAppConfiguration((app, config) =>
    {
        var assembly = typeof(Program).GetTypeInfo().Assembly;
        config.AddJsonFile(new EmbeddedFileProvider(assembly), "appsettings.json", optional: false, false);
    })
    .Build();

using var jvm = host.Services.GetRequiredService<IJavaMachine>();
//jvm.Load("D:/dev/TestJava/out/artifacts/TestJava_jar/TestJava.jar");
_ = await jvm.LoadAsync("D:/dev/JVMTest/JVMTest/out/artifacts/JVMTest_jar/JVMTest.jar");
await jvm.RunAsync();

await host.RunAsync();
