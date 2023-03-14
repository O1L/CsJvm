using CsJvm.Abstractions.Loader;
using CsJvm.Loader.Parsers;
using CsJvm.Models;
using Microsoft.Extensions.Logging;

namespace CsJvm.Loader
{
    /// <summary>
    /// JavaClass loader
    /// </summary>
    public class JavaClassLoader : IJavaClassLoader
    {
        /// <summary>
        /// Main logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// ClassFile loader
        /// </summary>
        private readonly IClassFileLoader _loader;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Main logger</param>
        /// <param name="loader">.class files loader</param>
        public JavaClassLoader(ILogger<JavaClassLoader> logger, IClassFileLoader loader)
        {
            _logger = logger;
            _loader = loader;
        }

        /// <inheritdoc/>
        public async Task<JavaClass?> LoadAsync(Stream stream, string className)
        {
            using var reader = new BinaryReader(stream);
            var classFile = await _loader.LoadAsync(reader);
            if (classFile == null)
                return null;

            try
            {
                var javaClass = new JavaClass(className, classFile)
                    .ParseMethods()
                    .ParseFields()
                    .ParseAttributes()
                    .ParseSuperclass();

                return javaClass;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot load class {className}", className);
                return null;
            }
        }
    }
}
