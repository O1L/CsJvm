using CsJvm.Models;

namespace CsJvm.Abstractions.VirtualMachine
{
    /// <summary>
    /// Provides methods to access classes from java runtime
    /// </summary>
    public interface IJavaRuntime
    {
        /// <summary>
        /// Gets class from runtime
        /// </summary>
        /// <param name="className">Class name</param>
        /// <returns>Loaded java class</returns>
        Task<JavaClass?> GetClassAsync(string className);
    }
}
