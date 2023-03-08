using CsJvm.Models.ClassFileFormat.Attributes;
using CsJvm.Models.ClassFileFormat.ConstantPool;
using CsJvm.Models.ClassFileFormat.Fields;
using CsJvm.Models.ClassFileFormat.Methods;

namespace CsJvm.Models.ClassFileFormat
{
    /// <summary>
    /// Class file format of the Java Virtual Machine
    /// </summary>
    public class ClassFile
    {
        /// <summary>
        /// The magic item supplies the magic number identifying the class file format; it has the value 0xCAFEBABE.
        /// </summary>
        public uint Magic { get; set; }

        /// <summary>
        /// The minor version numbers of this class file.
        /// </summary>
        public ushort MinorVersion { get; set; }

        /// <summary>
        /// The major version numbers of this class file.
        /// </summary>
        public ushort MajorVersion { get; set; }

        /// <summary>
        /// The value of the constant pool count item is equal to the number of entries in the constant_pool table plus one.
        /// </summary>
        public ushort ConstantPoolCount { get; set; }

        /// <summary>
        /// The constant_pool is a table of structures representing various string constants, class and interface names, 
        /// field names, and other constants that are referred to within the ClassFile structure and its substructures.
        /// </summary>
        public CpInfo[] ConstantPool { get; set; } = Array.Empty<CpInfo>();

        /// <summary>
        /// The value of the access_flags item is a mask of flags used to denote access permissions to and properties of this class or interface.
        /// </summary>
        public ClassAccessAndPropertyModifiers AccessFlags { get; set; }

        /// <summary>
        /// The value of the this_class item must be a valid index into the constant_pool table.
        /// </summary>
        public ushort ThisClass { get; set; }

        /// <summary>
        /// For a class, the value of the super_class item either must be zero or must be a valid index into the constant_pool table.
        /// </summary>
        public ushort SuperClass { get; set; }

        /// <summary>
        /// The value of the interfaces_count item gives the number of direct superinterfaces of this class or interface type.
        /// </summary>
        public ushort InterfacesCount { get; set; }

        /// <summary>
        /// Each value in the interfaces array must be a valid index into the constant_pool table. 
        /// </summary>
        public ushort[] Interfaces { get; set; } = Array.Empty<ushort>();

        /// <summary>
        /// The value of the fields_count item gives the number of field_info structures in the fields table. 
        /// </summary>
        public ushort FieldsCount { get; set; }

        /// <summary>
        /// Each value in the fields table must be a field_info structure giving a complete description of a field in this class or interface. 
        /// </summary>
        public FieldInfo[] Fields { get; set; } = Array.Empty<FieldInfo>();

        /// <summary>
        /// The value of the methods_count item gives the number of method_info structures in the methods table.
        /// </summary>
        public ushort MethodsCount { get; set; }

        /// <summary>
        /// Each value in the methods table must be a method_info structure (§4.6) giving a complete description of a method in this class or interface.
        /// </summary>
        public MethodInfo[] Methods { get; set; } = Array.Empty<MethodInfo>();

        /// <summary>
        /// The value of the attributes_count item gives the number of attributes in the attributes table of this class.
        /// </summary>
        public ushort AttributesCount { get; set; }

        /// <summary>
        /// Each value of the attributes table must be an attribute_info structure.
        /// </summary>
        public AttributeInfo[] Attributes { get; set; } = Array.Empty<AttributeInfo>();

        /// <summary>
        /// Internal binary reader to parse data
        /// </summary>
        public BinaryReader Reader { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="reader">Binary reader</param>
        public ClassFile(BinaryReader reader)
        {
            Reader = reader;
        }
    }
}
