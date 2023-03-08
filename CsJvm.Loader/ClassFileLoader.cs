using CsJvm.Abstractions.Loader;
using CsJvm.Loader.Parsers;
using CsJvm.Models.ClassFileFormat;
using Microsoft.Extensions.Logging;

namespace CsJvm.Loader
{
    /// <summary>
    /// A Java .class files loader
    /// </summary>
    public class ClassFileLoader : IClassFileLoader
    {
        /// <summary>
        /// Main logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Main logger</param>
        public ClassFileLoader(ILogger<ClassFileLoader> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool TryLoad(BinaryReader? reader, out ClassFile? classFile)
        {
            classFile = null;
            if (reader == null)
                return false;

            try
            {
                // the chain call order is important!
                classFile = new ClassFile(reader)
                    .ParseMagic()
                    .ParseVersion()
                    .ParseConstantPool()
                    .ParseClasses()
                    .ParseInterfaces()
                    .ParseFileds()
                    .ParseMethods()
                    .ParseAttributes();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on loading class file!");
                return false;
            }
        }
    }
}
