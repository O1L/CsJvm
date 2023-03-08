using CsJvm.Loader.Extensions;
using CsJvm.Models.ClassFileFormat;
using CsJvm.Models.ClassFileFormat.Attributes;
using CsJvm.Models.ClassFileFormat.ConstantPool;
using CsJvm.Models.ClassFileFormat.Fields;
using CsJvm.Models.ClassFileFormat.Methods;

namespace CsJvm.Loader.Parsers
{
    /// <summary>
    /// Provides methods to parse JVM class files
    /// </summary>
    public static class ClassFileParser
    {
        /// <summary>
        /// Parses magic data
        /// </summary>
        /// <param name="classFile">Class file</param>
        /// <param name="reader">BinaryReader reference</param>
        /// <returns><see cref="ClassFile"/> instance that can be used to chain call</returns>
        public static ClassFile ParseMagic(this ClassFile classFile)
        {
            classFile.Magic = classFile.Reader.ReadU4();

            if (classFile.Magic != 0xCAFEBABE)
                throw new InvalidDataException($"Bad magic: {classFile.Magic}");

            return classFile;
        }

        /// <summary>
        /// Parses version data
        /// </summary>
        /// <param name="classFile">Class file</param>
        /// <param name="reader">BinaryReader reference</param>
        /// <returns><see cref="ClassFile"/> instance that can be used to chain call</returns>
        public static ClassFile ParseVersion(this ClassFile classFile)
        {
            classFile.MinorVersion = classFile.Reader.ReadU2();
            classFile.MajorVersion = classFile.Reader.ReadU2();

            return classFile;
        }

        /// <summary>
        /// Parses ConstantPool data
        /// </summary>
        /// <param name="classFile">Class file</param>
        /// <param name="reader">BinaryReader reference</param>
        /// <returns><see cref="ClassFile"/> instance that can be used to chain call</returns>
        public static ClassFile ParseConstantPool(this ClassFile classFile)
        {
            classFile.ConstantPoolCount = classFile.Reader.ReadU2();
            if (classFile.ConstantPoolCount < 2)
                throw new IndexOutOfRangeException($"Bad constant pool count: {classFile.ConstantPoolCount}");

            // first (0-index) element not used
            classFile.ConstantPool = new CpInfo[classFile.ConstantPoolCount];

            // index range in constant_pool is [1; ...], 0 element skipped
            for (var index = 1; index < classFile.ConstantPoolCount; index++)
            {
                classFile.ConstantPool[index] = classFile.Reader.GetCpInfo();

                // If CONSTANT_Long_info or CONSTANT_Double_info structure is the entry at index 'n' in the constant_pool table, 
                // then the next usable entry in the table is located at index 'n+2'
                if (classFile.ConstantPool[index] is CONSTANT_LongInfo || classFile.ConstantPool[index] is CONSTANT_DoubleInfo)
                    index++;
            }

            return classFile;
        }

        /// <summary>
        /// Parses classes data
        /// </summary>
        /// <param name="classFile">Class file</param>
        /// <param name="reader">BinaryReader reference</param>
        /// <returns><see cref="ClassFile"/> instance that can be used to chain call</returns>
        public static ClassFile ParseClasses(this ClassFile classFile)
        {
            classFile.AccessFlags = (ClassAccessAndPropertyModifiers)classFile.Reader.ReadU2();

            classFile.ThisClass = classFile.Reader.ReadU2();
            if (classFile.ConstantPool[classFile.ThisClass] is not CONSTANT_ClassInfo)
                throw new InvalidDataException("The entry must be a CONSTANT_ClassInfo structure");

            classFile.SuperClass = classFile.Reader.ReadU2();
            if (classFile.SuperClass != 0 && classFile.ConstantPool[classFile.SuperClass] is not CONSTANT_ClassInfo)
                throw new InvalidDataException("The entry must be a CONSTANT_ClassInfo structure");

            return classFile;
        }

        /// <summary>
        /// Parses interfaces data
        /// </summary>
        /// <param name="classFile">Class file</param>
        /// <param name="reader">BinaryReader reference</param>
        /// <returns><see cref="ClassFile"/> instance that can be used to chain call</returns>
        public static ClassFile ParseInterfaces(this ClassFile classFile)
        {
            classFile.InterfacesCount = classFile.Reader.ReadU2();
            classFile.Interfaces = new ushort[classFile.InterfacesCount];
            for (var i = 0; i < classFile.InterfacesCount; i++)
            {
                var index = classFile.Reader.ReadU2();
                classFile.Interfaces[i] = index;
                if (classFile.ConstantPool[index] is not CONSTANT_ClassInfo)
                    throw new InvalidDataException("The entry must be a CONSTANT_ClassInfo structure");
            }

            return classFile;
        }

        /// <summary>
        /// Parses fileds data
        /// </summary>
        /// <param name="classFile">Class file</param>
        /// <param name="reader">BinaryReader reference</param>
        /// <returns><see cref="ClassFile"/> instance that can be used to chain call</returns>
        public static ClassFile ParseFileds(this ClassFile classFile)
        {
            classFile.FieldsCount = classFile.Reader.ReadU2();
            classFile.Fields = new FieldInfo[classFile.FieldsCount];
            for (var i = 0; i < classFile.FieldsCount; i++)
            {
                var accessFlags = classFile.Reader.ReadU2();
                var nameIndex = classFile.Reader.ReadU2();
                if (classFile.ConstantPool[nameIndex] is not CONSTANT_Utf8Info name)
                    throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                var descriptorIndex = classFile.Reader.ReadU2();
                if (classFile.ConstantPool[descriptorIndex] is not CONSTANT_Utf8Info descriptor)
                    throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                var attributesCount = classFile.Reader.ReadU2();
                var attributes = new AttributeInfo[attributesCount];

                for (var attr = 0; attr < attributesCount; attr++)
                    attributes[attr] = classFile.Reader.GetAttributeInfo(classFile.ConstantPool);

                classFile.Fields[i] = new()
                {
                    AccessFlags = (FieldAccessAndPropertyFlags)accessFlags,
                    NameIndex = nameIndex,
                    DescriptorIndex = descriptorIndex,
                    AttributesCount = attributesCount,
                    Attributes = attributes,
                    Name = name.Utf8String,
                    Descriptor = descriptor.Utf8String
                };
            }

            return classFile;
        }

        /// <summary>
        /// Parses methods data
        /// </summary>
        /// <param name="classFile">Class file</param>
        /// <param name="reader">BinaryReader reference</param>
        /// <returns><see cref="ClassFile"/> instance that can be used to chain call</returns>
        public static ClassFile ParseMethods(this ClassFile classFile)
        {
            classFile.MethodsCount = classFile.Reader.ReadU2();
            classFile.Methods = new MethodInfo[classFile.MethodsCount];

            for (var i = 0; i < classFile.MethodsCount; i++)
            {
                var accessFlags = classFile.Reader.ReadU2();
                var nameIndex = classFile.Reader.ReadU2();
                if (classFile.ConstantPool[nameIndex] is not CONSTANT_Utf8Info name)
                    throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                var descriptorIndex = classFile.Reader.ReadU2();
                if (classFile.ConstantPool[descriptorIndex] is not CONSTANT_Utf8Info descriptor)
                    throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                var attributesCount = classFile.Reader.ReadU2();
                var attributes = new AttributeInfo[attributesCount];

                for (var attr = 0; attr < attributesCount; attr++)
                    attributes[attr] = classFile.Reader.GetAttributeInfo(classFile.ConstantPool);

                classFile.Methods[i] = new MethodInfo
                {
                    AccessFlags = (MethodAccessAndPropertyFlags)accessFlags,
                    NameIndex = nameIndex,
                    DescriptorIndex = descriptorIndex,
                    AttributesCount = attributesCount,
                    Attributes = attributes,
                    Name = name.Utf8String,
                    Descriptor = descriptor.Utf8String
                };
            }

            return classFile;
        }

        /// <summary>
        /// Parses attributes data
        /// </summary>
        /// <param name="classFile">Class file</param>
        /// <param name="reader">BinaryReader reference</param>
        /// <returns><see cref="ClassFile"/> instance that can be used to chain call</returns>
        public static ClassFile ParseAttributes(this ClassFile classFile)
        {
            classFile.AttributesCount = classFile.Reader.ReadU2();
            classFile.Attributes = new AttributeInfo[classFile.AttributesCount];
            for (int i = 0; i < classFile.AttributesCount; i++)
                classFile.Attributes[i] = classFile.Reader.GetAttributeInfo(classFile.ConstantPool);

            return classFile;
        }
    }
}
