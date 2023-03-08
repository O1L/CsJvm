using CsJvm.Models;

namespace CsJvm.Abstractions.Loader
{
    public interface IJarLoader : IDisposable
    {
        /// <summary>
        /// Loaded java archive file
        /// </summary>
        JarFile? JAR { get; }

        /// <summary>
        /// Open specified JAR file (file handle keeps locked while application is using)
        /// </summary>
        /// <param name="path">Path to file</param>
        bool TryOpen(string path);

        /// <summary>
        /// Loads specified class from JAR
        /// </summary>
        /// <param name="className">Class to load</param>
        /// <returns><see langword="true"></see> if java class found and loaded; otherwise <see langword="false"></see></returns>
        bool TryGetClass(string className, out JavaClass? javaClass);
    }
}
