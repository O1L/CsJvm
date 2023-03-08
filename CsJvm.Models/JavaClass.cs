using CsJvm.Models.ClassFileFormat;
using CsJvm.Models.ClassFileFormat.Attributes;
using CsJvm.Models.ClassFileFormat.ConstantPool;

namespace CsJvm.Models
{
    /// <summary>
    /// Java class representation
    /// </summary>
    public class JavaClass
    {
        /// <summary>
        /// Class name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Access flags
        /// </summary>
        public ClassAccessAndPropertyModifiers AccessFlasg => ClassFile.AccessFlags;

        /// <summary>
        /// Internal <see cref="ClassFile"/> representation
        /// </summary>
        public ClassFile ClassFile { get; set; }

        /// <summary>
        /// True if the class is static and the ctor has already been called
        /// </summary>
        public bool StaticInitialized { get; set; }

        /// <summary>
        /// True if the default ctor has already been called
        /// </summary>
        public bool DefaulthInitialized { get; set; }

        /// <summary>
        /// Super class
        /// </summary>
        public JavaClass? SuperClass { get; set; }

        /// <summary>
        /// Constant pool info
        /// </summary>
        public CpInfo[] ConstantPool => ClassFile.ConstantPool;

        /// <summary>
        /// Fields, key is a field name
        /// </summary>
        public Dictionary<string, object?> Fields { get; set; } = new();

        /// <summary>
        /// Methods, key is a method name
        /// </summary>
        public Dictionary<string, JavaMethod> Methods { get; set; } = new();

        /// <summary>
        /// Bootstrap methods, key is a method name
        /// </summary>
        public Dictionary<string, BootstrapMethod> BootstrapMethods { get; set; } = new();

        /// <summary>
        /// Attributes info
        /// </summary>
        public AttributeInfo[] Attributes { get; set; } = Array.Empty<AttributeInfo>();

        /// <summary>
        /// Inner classes
        /// </summary>
        public List<string> InnerClasses { get; set; } = new();

        /// <summary>
        /// Outer classes
        /// </summary>
        public List<string> OuterClasses { get; set; } = new();

        /// <summary>
        /// Implementations of this class
        /// </summary>
        public HashSet<JavaClass> Implementations { get; set; } = new();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">Class name</param>
        /// <param name="classFile">.class file</param>
        public JavaClass(string name, ClassFile classFile)
        {
            Name = name;
            ClassFile = classFile;
        }
    }
}
