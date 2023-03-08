using CsJvm.Abstractions.Loader;
using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models;
using Microsoft.Extensions.Logging;

namespace CsJvm.VirtualMachine
{
    /// <summary>
    /// Java Virtual Machine implementation
    /// </summary>
    public class JavaMachine : IJavaMachine
    {
        /// <summary>
        /// Main logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Jar loader
        /// </summary>
        private readonly IJarLoader _loader;

        /// <summary>
        /// Opcodes decoder
        /// </summary>
        private readonly IOpcodesDecoder _decoder;

        /// <summary>
        /// Flag to check the dispose state
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Current frame
        /// </summary>
        private Frame? _frame;

        /// <summary>
        /// Main thread ref
        /// </summary>
        private IJavaThread? _mainThread;

        /// <inheritdoc/>
        public IJavaThread? MainThread => _mainThread;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Main logger</param>
        /// <param name="loader">Jar loader</param>
        /// <param name="decoder">Opcodes decoder</param>
        public JavaMachine(ILogger<JavaMachine> logger, IJarLoader loader, IOpcodesDecoder decoder)
        {
            _logger = logger;
            _loader = loader;
            _decoder = decoder;
        }

        /// <inheritdoc/>
        public void Load(string path)
        {
            if (!_loader.TryOpen(path))
            {
                _logger.LogCritical("Cannot open file as a JAR by path={path}", path);
                return;
            }

            // looking for main class
            var mainClassName = _loader.JAR?.Manifest.MainClass;
            if (string.IsNullOrEmpty(mainClassName) || !_loader.TryGetClass(mainClassName, out var mainClass) || mainClass == null)
            {
                _logger.LogCritical("Cannot find main class!");
                return;
            }

            // looking for main method
            if (!mainClass.Methods.TryGetValue("main:([Ljava/lang/String;)V", out var method)
                || method == null
                || method.CodeAttribute == null)
            {
                _logger.LogCritical("Cannot find main method!");
                return;
            }

            // creata a method frame
            _frame = new Frame
            {
                Code = method.CodeAttribute.Code,
                CpInfo = mainClass.ConstantPool,
                LocalVariables = new object[method.CodeAttribute.MaxLocals],
                MethodName = method.Name
            };

            // create main thread
            _mainThread = new JavaThread(_decoder, _frame);
        }

        /// <inheritdoc/>
        public void Run()
        {
            _mainThread?.Run();
        }

        /// <inheritdoc/>
        public void PauseResume() => throw new NotImplementedException();

        /// <inheritdoc/>
        public void Stop() => throw new NotImplementedException();

        /// <inheritdoc/>
        public void StepInto() => _mainThread?.StepInto();

        /// <inheritdoc/>
        public void StepOver() => _mainThread?.StepOver();

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes manager resources
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _loader?.Dispose();

            _disposed = true;
        }
    }
}
