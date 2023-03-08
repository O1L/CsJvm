using CsJvm.Abstractions.Loader;
using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CsJvm.VirtualMachine
{
    /// <summary>
    /// Java runtime accessor
    /// </summary>
    public class JavaRuntime : IJavaRuntime
    {
        /// <summary>
        /// Main logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Current configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Jar loader
        /// </summary>
        private readonly IJarLoader _loader;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Main logger</param>
        /// <param name="configuration">Current configuration</param>
        /// <param name="loader">Jar loader</param>
        public JavaRuntime(ILogger<JavaRuntime> logger, IConfiguration configuration, IJarLoader loader)
        {
            _logger = logger;
            _loader = loader;
            _configuration = configuration;

            if (!TryLoadRuntime(configuration))
                _logger.LogCritical("Cannot load java runtime library!");
        }

        /// <inheritdoc/>
        public bool TryGet(string className, out JavaClass? javaClass)
            => _loader.TryGetClass(className, out javaClass);

        /// <summary>
        /// Tries load java runtime
        /// </summary>
        /// <param name="configuration">Current configuration</param>
        /// <returns><see langword="true"></see> if success; otherwise <see langword="false"></see></returns>
        private bool TryLoadRuntime(IConfiguration configuration)
        {
            try
            {
                var path = configuration.GetValue<string>("JavaRuntime:Path");
                if (string.IsNullOrWhiteSpace(path))
                {
                    _logger.LogError("The java runtime path not specified in appconfig.json");
                    return false;
                }

                var rtPath = Path.Combine(path, "rt.jar");
                return _loader.TryOpen(rtPath);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Exception when loading java runtime");
                return false;
            }
        }
    }
}
