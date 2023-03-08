using CsJvm.Models.ClassFileFormat.ConstantPool;

namespace CsJvm.Disasm.DisasmDescriptions
{
    /// <summary>
    /// Const Info Description
    /// </summary>
    public class ConstInfoDescription : DisasmDescriptionsBase
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="index"><Index in constant pool array/param>
        /// <param name="cpInfo">Constant pool array</param>
        public ConstInfoDescription(int index, CpInfo[] cpInfo) : base(index, cpInfo)
        {
        }

        /// <inheritdoc/>
        public override string GetInfo() =>
            CpInfo[Index] switch
            {
                CONSTANT_ClassInfo classInfo => $"// class {((CONSTANT_Utf8Info)CpInfo[classInfo.NameIndex]).Utf8String}",
                CONSTANT_StringInfo stringInfo => $"// String {((CONSTANT_Utf8Info)CpInfo[stringInfo.StringIndex]).Utf8String}",
                CONSTANT_IntegerInfo integerInfo => $"// int {integerInfo.Value}",
                CONSTANT_FloatInfo floatInfo => $"// float {floatInfo.Value:0.0######}",
                CONSTANT_LongInfo longInfo => $"// long {longInfo.Value}",
                CONSTANT_DoubleInfo doubleInfo => $"// double {doubleInfo.Value:0.0###############}d",

                _ => $"// unknown {CpInfo[Index].GetType().Name} const value"
            };
    }
}
