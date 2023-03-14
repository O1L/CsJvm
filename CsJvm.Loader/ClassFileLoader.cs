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
        public Task<ClassFile?> LoadAsync(BinaryReader? reader)
        {
            if (reader == null)
                return Task.FromResult<ClassFile?>(null);

            return Task.Run<ClassFile?>(() =>
            {
                // the chain call order is important!
                return new ClassFile(reader)
                    .ParseMagic()
                    .ParseVersion()
                    .ParseConstantPool()
                    .ParseClasses()
                    .ParseInterfaces()
                    .ParseFileds()
                    .ParseMethods()
                    .ParseAttributes();
            });
        }
    }
}
