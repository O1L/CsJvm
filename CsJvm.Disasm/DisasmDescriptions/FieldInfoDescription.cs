using CsJvm.Models.ClassFileFormat.ConstantPool;

namespace CsJvm.Disasm.DisasmDescriptions
{
    /// <summary>
    /// Field Info Description
    /// </summary>
    public class FieldInfoDescription : DisasmDescriptionsBase
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="index"><Index in constant pool array/param>
        /// <param name="cpInfo">Constant pool array</param>
        public FieldInfoDescription(int index, CpInfo[] cpInfo) : base(index, cpInfo)
        {
        }

        /// <inheritdoc/>
        public override string GetInfo()
        {
            if (CpInfo[Index] is not CONSTANT_FieldrefInfo info)
                throw new InvalidOperationException();

            var nameAndTypeInfo = (CONSTANT_NameAndTypeInfo)CpInfo[info.NameAndTypeIndex];
            var classInfo = (CONSTANT_ClassInfo)CpInfo[info.ClassIndex];

            var className = ((CONSTANT_Utf8Info)CpInfo[classInfo.NameIndex]).Utf8String;
            var fieldName = ((CONSTANT_Utf8Info)CpInfo[nameAndTypeInfo.NameIndex]).Utf8String;
            var descriptor = ((CONSTANT_Utf8Info)CpInfo[nameAndTypeInfo.DescriptorIndex]).Utf8String;
            var field = $"{fieldName}:{descriptor}";

            return $"// Field {field}";
        }
    }
}
