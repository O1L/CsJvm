using System.Reflection;
namespace CsJvm.Models
{
    /// <summary>
    /// Native method structure
    /// </summary>
    public class NativeMethod
    {
        /// <summary>
        /// Owner class name
        /// </summary>
        public string ClassName { get; set; } = string.Empty;

        /// <summary>
        /// Method name
        /// </summary>
        public string MethodName { get; set; } = string.Empty;

        /// <summary>
        /// Method descriptor
        /// </summary>
        public string Descriptor { get; set; } = string.Empty;

        /// <summary>
        /// Method owner instance
        /// </summary>
        public object? Instance { get; set; }

        /// <summary>
        /// Native method body
        /// </summary>
        public MethodInfo? Method { get; set; }

        /// <summary>
        /// Method arguments
        /// </summary>
        public object[] Args { get; set; } = Array.Empty<object>();
    }
}
