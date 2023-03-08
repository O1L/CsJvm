using CsJvm.Models.ClassFileFormat.ConstantPool;

namespace CsJvm.Disasm.DisasmDescriptions
{
    /// <summary>
    /// Provides disassmebled description info
    /// </summary>
    public abstract class DisasmDescriptionsBase
    {
        /// <summary>
        /// Index in constant pool array
        /// </summary>
        protected readonly int Index;

        /// <summary>
        /// Constant pool array
        /// </summary>
        protected readonly CpInfo[] CpInfo;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="index"><Index in constant pool array/param>
        /// <param name="cpInfo">Constant pool array</param>
        public DisasmDescriptionsBase(int index, CpInfo[] cpInfo)
        {
            Index = index;
            CpInfo = cpInfo;
        }

        /// <summary>
        /// Gets description information as string
        /// </summary>
        /// <returns></returns>
        public abstract string GetInfo();
    }
}
