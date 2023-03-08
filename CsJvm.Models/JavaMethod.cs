using CsJvm.Models.ClassFileFormat.Attributes;
using CsJvm.Models.ClassFileFormat.Methods;
using CsJvm.Models.Disasm;

namespace CsJvm.Models
{
    public class JavaMethod
    {
        public string Name { get; set; } = string.Empty;

        public MethodAccessAndPropertyFlags AccessFlags { get; set; }

        public CodeAttribute? CodeAttribute { get; set; }


        /// <summary>
        /// Disassembled opcodes info
        /// </summary>
        public OpcodeInfo[] Opcodes { get; set; } = Array.Empty<OpcodeInfo>();
    }
}
