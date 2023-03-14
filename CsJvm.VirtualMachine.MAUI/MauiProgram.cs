using CsJvm.Abstractions.Disasm;
using CsJvm.Abstractions.Instructions;
using CsJvm.Abstractions.Loader;
using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Disasm;
using CsJvm.Loader;
using CsJvm.VirtualMachine.Heap;
using CsJvm.VirtualMachine.Instructions.Interpreter;
using CsJvm.VirtualMachine.MAUI.PageModels;
using CsJvm.VirtualMachine.MAUI.Pages;
using FreshMvvm.Maui.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CsJvm.VirtualMachine.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CsJvm.VirtualMachine.MAUI.appsettings.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

            var builder = MauiApp.CreateBuilder();

            // add configuration
            builder.Configuration.AddConfiguration(config);

            builder.Services

                // add logger to debug console
                .AddLogging(configure => configure.AddDebug())

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
                .AddSingleton<Natives.Java.Lang.Class>()
                .AddSingleton<Natives.Java.Lang.Object>()
                .AddSingleton<Natives.Java.Lang.String>()
                .AddSingleton<Natives.Java.Lang.System>()

                // java machine
                .AddSingleton<IJavaHeap, JavaHeap>()
                .AddSingleton<IJavaExecutable, JavaExecutable>()
                .AddSingleton<IJavaRuntime, JavaRuntime>()
                .AddSingleton<IJavaMachine, JavaMachine>()

                // Pages
                .AddTransient<MainPage>()

                // page models
                .AddTransient<MainPageModel>();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Courier-New.ttf", "CourierNew");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();
            app.UseFreshMvvm();

            return app;
        }
    }
}