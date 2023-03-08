using CsJvm.Models.ClassFileFormat.ConstantPool;

namespace CsJvm.Models
{
    /// <summary>
    /// A frame is used to store data and partial results, as well as to perform dynamic linking, return values for methods, and dispatch exceptions.
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// An array of variables known as local variables
        /// <para>A single local variable can hold a value of type boolean, byte, char, short, int, float, reference, or returnAddress.</para>
        /// <para>A pair of local variables can hold a value of type long or double.</para>
        /// </summary>
        public object?[] LocalVariables { get; set; } = Array.Empty<object?>();

        /// <summary>
        /// A last-in-first-out (LIFO) stack known as operand stack
        /// </summary>
        public Stack<object?> OperandStack { get; set; } = new();

        /// <summary>
        /// A reference to the run-time constant pool of the class of the current method
        /// </summary>
        public CpInfo[] CpInfo { get; set; } = Array.Empty<CpInfo>();

        /// <summary>
        /// Method bytecode
        /// </summary>
        public byte[] Code { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Method name
        /// </summary>
        public string MethodName { get; set; } = string.Empty;
    }
}
