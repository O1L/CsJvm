using CsJvm.Models;

namespace CsJvm.Abstractions.Loader
{
    /// <summary>
    /// Provides ablity to parse and load Java class files in the virtual machine internal representation
    /// </summary>
    public interface IJavaClassLoader
    {
        /// <summary>
        /// Loads specified class
        /// </summary>
        /// <param name="stream">Stream to load</param>
        /// <param name="className">Class name</param>
        /// <returns>Loaded java class instance</returns>
        Task<JavaClass?> LoadAsync(Stream stream, string className);
    }
}
