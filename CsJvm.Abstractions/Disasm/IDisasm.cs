using CsJvm.Models;
using CsJvm.Models.ClassFileFormat.ConstantPool;
using CsJvm.Models.Disasm;

namespace CsJvm.Abstractions.Disasm
{
    /// <summary>
    /// Provides ability to disassemby .class files
    /// </summary>
    public interface IDisasm
    {
        /// <summary>
        /// Gets disassmebled instructions
        /// </summary>
        /// <param name="code">Bytecode to disassemble</param>
        /// <param name="info">Constant pool info</param>
        /// <returns>An array of <see cref="OpcodeInfo"/></returns>
        OpcodeInfo[] GetOpcodes(byte[]? code, CpInfo[] info);

        /// <summary>
        /// Gets disassembled code as a human-readable text
        /// </summary>
        /// <param name="method">Method to disassemble</param>
        /// <param name="info">Constant pool info</param>
        /// <returns></returns>
        string GetDisasmString(JavaMethod method, CpInfo[] info);
    }
}
