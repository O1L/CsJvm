using CsJvm.Models;

namespace CsJvm.VirtualMachine.Extensions
{
    /// <summary>
    /// NativeMethod extensions
    /// </summary>
    public static class NativeMethodExtensions
    {
        /// <summary>
        /// Creates a deep clone of current instance
        /// </summary>
        /// <param name="frame">Method to clone</param>
        /// <returns>A new <see cref="NativeMethodE"/> clone</returns>
        public static NativeMethod Clone(this NativeMethod frame)
        {
            return new()
            {
                ClassName = frame.ClassName,
                MethodName = frame.MethodName,
                Descriptor = frame.Descriptor,
                Instance = frame.Instance,
                Args = (object[])frame.Args.Clone(),
                Method = frame.Method
            };
        }
    }
}
