using CsJvm.Models;

namespace CsJvm.Abstractions.VirtualMachine
{
    /// <summary>
    /// Provides methods to get native methods implementations
    /// </summary>
    public interface INativeMethodsProvider
    {
        /// <summary>
        /// Gets native method implementation
        /// </summary>
        /// <param name="className">Class name</param>
        /// <param name="methodName">Method name</param>
        /// <param name="descriptor">Method desriptor</param>
        /// <param name="method">Result value</param>
        /// <param name="args">Method args</param>
        /// <returns><see langword="true"></see> if success; otherwise <see langword="false"></see></returns>
        bool TryGetMethod(string className, string methodName, string descriptor, object[] args, out NativeMethod? method);
    }
}
