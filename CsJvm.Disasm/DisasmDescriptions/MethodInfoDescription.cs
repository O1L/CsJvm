using CsJvm.Models.ClassFileFormat.ConstantPool;

namespace CsJvm.Disasm.DisasmDescriptions
{
    /// <summary>
    /// Method Info Description
    /// </summary>
    public class MethodInfoDescription : DisasmDescriptionsBase
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="index"><Index in constant pool array/param>
        /// <param name="cpInfo">Constant pool array</param>
        public MethodInfoDescription(int index, CpInfo[] cpInfo) : base(index, cpInfo)
        {
        }

        /// <inheritdoc/>
        public override string GetInfo()
        {
            switch (CpInfo[Index])
            {
                case CONSTANT_MethodrefInfo info:
                    {
                        var classInfo = (CONSTANT_ClassInfo)CpInfo[info.Classindex];
                        var nameAndTypeInfo = (CONSTANT_NameAndTypeInfo)CpInfo[info.NameAndTypeIndex];

                        var className = ((CONSTANT_Utf8Info)CpInfo[classInfo.NameIndex]).Utf8String;
                        var methodName = ((CONSTANT_Utf8Info)CpInfo[nameAndTypeInfo.NameIndex]).Utf8String;
                        var descriptor = ((CONSTANT_Utf8Info)CpInfo[nameAndTypeInfo.DescriptorIndex]).Utf8String;

                        return $"// Method {className}.{methodName}:{descriptor}";
                    }

                case CONSTANT_InterfaceMethodrefInfo interfaceInfo:
                    {
                        var classInfo = (CONSTANT_ClassInfo)CpInfo[interfaceInfo.ClassIndex];
                        var nameAndTypeInfo = (CONSTANT_NameAndTypeInfo)CpInfo[interfaceInfo.NameAndTypeIndex];

                        var className = ((CONSTANT_Utf8Info)CpInfo[classInfo.NameIndex]).Utf8String;
                        var methodName = ((CONSTANT_Utf8Info)CpInfo[nameAndTypeInfo.NameIndex]).Utf8String;
                        var descriptor = ((CONSTANT_Utf8Info)CpInfo[nameAndTypeInfo.DescriptorIndex]).Utf8String;

                        return $"// InterfaceMethod {className}.{methodName}:{descriptor}";
                    }

                case CONSTANT_InvokeDynamicInfo idInfo:
                    {
                        var nameAndTypeInfo = (CONSTANT_NameAndTypeInfo)CpInfo[idInfo.NameAndTypeIndex];
                        var methodName = ((CONSTANT_Utf8Info)CpInfo[nameAndTypeInfo.NameIndex]).Utf8String;
                        var descriptor = ((CONSTANT_Utf8Info)CpInfo[nameAndTypeInfo.DescriptorIndex]).Utf8String;

                        return $"// DynamicMethod {methodName}:{descriptor}";
                    }

                default:
                    throw new InvalidOperationException($"// Unknown info: {CpInfo[Index].GetType()}");
            }
        }
    }
}
