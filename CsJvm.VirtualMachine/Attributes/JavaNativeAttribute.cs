namespace CsJvm.VirtualMachine.Attributes
{
    /// <summary>
    /// Native method attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class JavaNativeAttribute : Attribute
    {
        /// <summary>
        /// Owner class name
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Method name
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Method descriptor
        /// </summary>
        public string Descriptor { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="className">Owner class name</param>
        /// <param name="methodName">Method name</param>
        /// <param name="descriptor">Method descriptor</param>
        public JavaNativeAttribute(string className, string methodName, string descriptor)
        {
            ClassName = className;
            MethodName = methodName;
            Descriptor = descriptor;
        }
    }
}
