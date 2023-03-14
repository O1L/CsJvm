using CsJvm.Abstractions.Loader;
using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models;
using Microsoft.Extensions.Logging;

namespace CsJvm.VirtualMachine
{
    /// <summary>
    /// loaded executable JAR file handler
    /// </summary>
    public class JavaExecutable : IJavaExecutable
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
        /// Flag to check the dispose state
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Main class
        /// </summary>
        private JavaClass? _mainClass;

        /// <inheritdoc/>
        public JavaClass? MainClass => _mainClass;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="loader"></param>
        public JavaExecutable(ILogger<JavaExecutable> logger, IJarLoader loader)
        {
            _logger = logger;
            _loader = loader;
        }

        /// <inheritdoc/>
        public async Task<bool> LoadAsync(string path)
        {
            if (!await _loader.OpenAsync(path))
            {
                _logger.LogCritical("Cannot open file as a JAR by path={path}", path);
                return false;
            }

            // looking for main class
            var mainClassName = _loader.JAR?.Manifest.MainClass;
            if (string.IsNullOrEmpty(mainClassName) || (_mainClass = await _loader.GetClassAsync(mainClassName)) == null)
            {
                _logger.LogCritical("Cannot find main class!");
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public Task<JavaClass?> GetClassAsync(string className)
            => _loader.GetClassAsync(className);

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
