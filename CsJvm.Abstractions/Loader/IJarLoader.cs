using CsJvm.Models;

namespace CsJvm.Abstractions.Loader
{
    /// <summary>
    /// Provides method to load JAR files
    /// </summary>
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
        /// <returns><see langword="true"></see> if java class found and opened; otherwise <see langword="false"></see></returns>
        Task<bool> OpenAsync(string path);

        /// <summary>
        /// Loads specified class from JAR
        /// </summary>
        /// <param name="className">Class to load</param>
        /// <returns>Loaded class</returns>
        Task<JavaClass?> GetClassAsync(string className);
    }
}
