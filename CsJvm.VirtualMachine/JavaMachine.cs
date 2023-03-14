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
        /// Executable jar loader
        /// </summary>
        private readonly IJavaExecutable _executable;

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
        /// <param name="executable">Executable jar loader</param>
        /// <param name="decoder">Opcodes decoder</param>
        public JavaMachine(ILogger<JavaMachine> logger, IJavaExecutable executable, IOpcodesDecoder decoder)
        {
            _logger = logger;
            _executable = executable;
            _decoder = decoder;
        }

        /// <inheritdoc/>
        public async Task<bool> LoadAsync(string path)
        {
            if (!await _executable.LoadAsync(path))
                return false;

            // looking for main method
            if (_executable.MainClass == null
                || !_executable.MainClass.Methods.TryGetValue("main:([Ljava/lang/String;)V", out var method)
                || method == null
                || method.CodeAttribute == null)
            {
                _logger.LogCritical("Cannot find main method!");
                return false;
            }

            // creata a method frame
            _frame = new Frame
            {
                Code = method.CodeAttribute.Code,
                CpInfo = _executable.MainClass.ConstantPool,
                LocalVariables = new object[method.CodeAttribute.MaxLocals],
                MethodName = method.Name
            };

            // create main thread
            _mainThread = new JavaThread(_decoder, _frame);
            return true;
        }

        /// <inheritdoc/>
        public Task RunAsync()
        {
            if (_mainThread == null)
                return Task.CompletedTask;

            return _mainThread.RunAsync();
        }

        /// <inheritdoc/>
        public void PauseResume() => throw new NotImplementedException();

        /// <inheritdoc/>
        public void Stop() => throw new NotImplementedException();

        /// <inheritdoc/>
        public Task StepIntoAsync()
        {
            if (_mainThread == null)
                return Task.CompletedTask;

            return _mainThread.StepIntoAsync();
        }

        /// <inheritdoc/>
        public Task StepOverAsync()
        {
            if (_mainThread == null)
                return Task.CompletedTask;

            return _mainThread.StepOverAsync();
        }

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
                _executable?.Dispose();

            _disposed = true;
        }
    }
}
