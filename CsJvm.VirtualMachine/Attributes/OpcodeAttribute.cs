namespace CsJvm.VirtualMachine.Attributes
{
    /// <summary>
    /// Opcode attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class OpcodeAttribute : Attribute
    {
        /// <summary>
        /// Opcode signature
        /// </summary>
        public byte Opcode { get; set; }

        /// <summary>
        /// Opcode name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="opcode">Opcode signature</param>
        /// <param name="name">Opcode name</param>
        public OpcodeAttribute(byte opcode, string name)
        {
            Opcode = opcode;
            Name = name;
        }
    }
}
