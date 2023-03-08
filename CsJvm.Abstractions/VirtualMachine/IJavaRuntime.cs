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
        /// <param name="javaClass">Result reference</param>
        /// <returns><see langword="true"></see> if success; otherwise <see langword="false"></see></returns>
        bool TryGet(string className, out JavaClass? javaClass);
    }
}
