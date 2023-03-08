using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models;
using CsJvm.VirtualMachine.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Reflection;

namespace CsJvm.VirtualMachine
{
    /// <summary>
    /// Provides native methods
    /// </summary>
    public class NativeMethodsProvider : INativeMethodsProvider
    {
        /// <summary>
        /// Current service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Native methods implementations
        /// </summary>
        private readonly ConcurrentDictionary<string, (object Instance, MethodInfo Method)> _nativeMethods = new();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        public NativeMethodsProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Initialize();
        }

        /// <inheritdoc/>
        public bool TryGetMethod(string className, string methodName, string descriptor, object[] args, out NativeMethod? method)
        {
            method = null;
            var key = $"{className}.{methodName}:{descriptor}";

            if (!_nativeMethods.TryGetValue(key, out var nativeMethod))
                return false;

            method = new()
            {
                ClassName = className,
                MethodName = methodName,
                Descriptor = descriptor,
                Instance = nativeMethod.Instance,
                Method = nativeMethod.Method,
                Args = args
            };

            return true;
        }

        /// <summary>
        /// Collects native methods implementations using reflection
        /// </summary>
        private void Initialize()
        {
            var natives = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsAssignableTo(typeof(INativeCall)) && !t.IsInterface).ToArray();

            foreach (var native in natives)
            {
                var instance = _serviceProvider.GetRequiredService(native);

                Array.ForEach(instance.GetType().GetMethods(), method =>
                {
                    var ca = method.GetCustomAttribute<JavaNativeAttribute>();
                    if (ca != null)
                    {
                        var key = $"{ca.ClassName}.{ca.MethodName}:{ca.Descriptor}";
                        _nativeMethods.TryAdd(key, (instance, method));
                    }
                });
            }
        }
    }
}
