using CsJvm.Models.ClassFileFormat.ConstantPool;
using CsJvm.Models.Disasm;

namespace CsJvm.Abstractions.Disasm
{
    /// <summary>
    /// Provides a methods to find disassembled description data
    /// </summary>
    public interface IDisasmDescriptionProvider
    {
        /// <summary>
        /// Gets disassembled description as string
        /// </summary>
        /// <param name="infoType">Info type</param>
        /// <param name="index">Index to look at constant pool</param>
        /// <param name="cpInfo">Constant pool info</param>
        /// <returns></returns>
        string GetInfo(DisasmInfoType infoType, int index, CpInfo[] cpInfo);
    }
}
