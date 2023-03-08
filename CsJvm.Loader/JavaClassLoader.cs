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

        /// <summary>
        /// Loads <see cref="JavaClass"/> from stream
        /// </summary>
        /// <param name="stream">Stream to load</param>
        /// <returns>Loaded <see cref="JavaClass"/> instance</returns>
        public bool TryLoad(Stream stream, string className, out JavaClass? javaClass)
        {
            javaClass = null;

            using var reader = new BinaryReader(stream);
            if (!_loader.TryLoad(reader, out var classFile) || classFile == null)
                return false;

            try
            {
                javaClass = new JavaClass(className, classFile)
                    .ParseMethods()
                    .ParseFields()
                    .ParseAttributes()
                    .ParseSuperclass();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot load class {className}", className);
                return false;
            }
        }
    }
}
