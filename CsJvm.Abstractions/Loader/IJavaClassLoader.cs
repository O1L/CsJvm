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
        /// <param name="javaClass">Result class</param>
        /// <returns><see langword="true"></see> if success; otherwise <see langword="false"></see></returns>
        bool TryLoad(Stream stream, string className, out JavaClass? javaClass);
    }
}
