namespace CsJvm.Models.Disasm
{
    /// <summary>
    /// Disassembled method info
    /// </summary>
    public class DisasmMethodInfo
    {
        /// <summary>
        /// Method name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Method descriptor
        /// </summary>
        public string Descriptor { get; set; } = string.Empty;

        /// <summary>
        /// Disassembled instructions
        /// </summary>
        public OpcodeInfo[] Opcodes { get; set; } = Array.Empty<OpcodeInfo>();
    }
}
