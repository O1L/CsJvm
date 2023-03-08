using CsJvm.Loader.Extensions;
using CsJvm.Models.ClassFileFormat.ConstantPool;

namespace CsJvm.Loader.Parsers
{
    /// <summary>
    /// ConstantPool Parser
    /// </summary>
    public static class ConstantPoolParser
    {
        /// <summary>
        /// Parse <see cref="ConstantPoolInfo"/> from current <see cref="BinaryReader"/> instance
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static CpInfo GetCpInfo(this BinaryReader reader)
        {
            var tag = (ConstantPoolTag)reader.ReadU1();

            CpInfo info = tag switch
            {
                ConstantPoolTag.CONSTANT_Class => new CONSTANT_ClassInfo
                {
                    Tag = tag,
                    NameIndex = reader.ReadU2()
                },

                ConstantPoolTag.CONSTANT_Fieldref => new CONSTANT_FieldrefInfo
                {
                    Tag = tag,
                    ClassIndex = reader.ReadU2(),
                    NameAndTypeIndex = reader.ReadU2()
                },

                ConstantPoolTag.CONSTANT_Methodref => new CONSTANT_MethodrefInfo
                {
                    Tag = tag,
                    Classindex = reader.ReadU2(),
                    NameAndTypeIndex = reader.ReadU2()
                },

                ConstantPoolTag.CONSTANT_InterfaceMethodref => new CONSTANT_InterfaceMethodrefInfo
                {
                    Tag = tag,
                    ClassIndex = reader.ReadU2(),
                    NameAndTypeIndex = reader.ReadU2()
                },

                ConstantPoolTag.CONSTANT_String => new CONSTANT_StringInfo
                {
                    Tag = tag,
                    StringIndex = reader.ReadU2()
                },

                ConstantPoolTag.CONSTANT_Integer => new CONSTANT_IntegerInfo
                {
                    Tag = tag,
                    Bytes = reader.ReadU4()
                },

                ConstantPoolTag.CONSTANT_Float => new CONSTANT_FloatInfo
                {
                    Tag = tag,
                    Bytes = reader.ReadU4()
                },

                ConstantPoolTag.CONSTANT_Long => new CONSTANT_LongInfo
                {
                    Tag = tag,
                    HighBytes = reader.ReadU4(),
                    LowBytes = reader.ReadU4()
                },

                ConstantPoolTag.CONSTANT_Double => new CONSTANT_DoubleInfo
                {
                    Tag = tag,
                    HighBytes = reader.ReadU4(),
                    LowBytes = reader.ReadU4()
                },

                ConstantPoolTag.CONSTANT_NameAndType => new CONSTANT_NameAndTypeInfo
                {
                    Tag = tag,
                    NameIndex = reader.ReadU2(),
                    DescriptorIndex = reader.ReadU2()
                },

                ConstantPoolTag.CONSTANT_Utf8 => new CONSTANT_Utf8Info
                {
                    Tag = tag,
                    Length = reader.ReadU2()
                },

                ConstantPoolTag.CONSTANT_MethodHandle => new CONSTANT_MethodHandleInfo
                {
                    Tag = tag,
                    ReferenceKind = reader.ReadU1(),
                    ReferenceIndex = reader.ReadU2()
                },

                ConstantPoolTag.CONSTANT_MethodType => new CONSTANT_MethodTypeInfo
                {
                    Tag = tag,
                    DescriptorIndex = reader.ReadU2()
                },

                ConstantPoolTag.CONSTANT_Dynamic => new CONSTANT_DynamicInfo
                {
                    Tag = tag,
                    BootstrapMethodAttrIndex = reader.ReadU2(),
                    NameAndTypeIndex = reader.ReadU2()
                },

                ConstantPoolTag.CONSTANT_InvokeDynamic => new CONSTANT_InvokeDynamicInfo
                {
                    Tag = tag,
                    BootstrapMethodAttrIndex = reader.ReadU2(),
                    NameAndTypeIndex = reader.ReadU2()
                },

                ConstantPoolTag.CONSTANT_Module => new CONSTANT_ModuleInfo
                {
                    Tag = tag,
                    NameIndex = reader.ReadU2()
                },

                ConstantPoolTag.CONSTANT_Package => new CONSTANT_PackageInfo
                {
                    Tag = tag,
                    NameIndex = reader.ReadU2()
                },

                _ => throw new InvalidDataException($"Unknown tag: {tag}")
            };

            if (info is CONSTANT_Utf8Info utf8Info)
                utf8Info.Bytes = reader.ReadBytes(utf8Info.Length);

            return info;
        }
    }
}
