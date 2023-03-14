using CsJvm.Models;

namespace CsJvm.Abstractions.VirtualMachine
{
    /// <summary>
    /// Provides methods to access classes from loaded executable jar
    /// </summary>
    public interface IJavaExecutable : IDisposable
    {
        /// <summary>
        /// Main class reference
        /// </summary>
        JavaClass? MainClass { get; }

        /// <summary>
        /// Loads JAR file to execute
        /// </summary>
        /// <param name="path"></param>
        /// <returns><see langword="true"></see> if success; otherwise <see langword="false"></see></returns>
        Task<bool> LoadAsync(string path);

        /// <summary>
        /// Gets method from loaded executable file
        /// </summary>
        /// <param name="className">Class name</param>
        /// <returns>Loaded class</returns>
        Task<JavaClass?> GetClassAsync(string className);
    }
}
