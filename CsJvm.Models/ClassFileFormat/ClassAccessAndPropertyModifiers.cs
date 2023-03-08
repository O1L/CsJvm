namespace CsJvm.Models.ClassFileFormat
{
    /// <summary>
    /// Class access and property modifiers
    /// </summary>
    [Flags]
    public enum ClassAccessAndPropertyModifiers
    {
        /// <summary>
        /// Declared public; may be accessed from outside its package.
        /// </summary>
        ACC_PUBLIC = 0x0001,

        /// <summary>
        /// Declared final; no subclasses allowed.
        /// </summary>
        ACC_FINAL = 0x0010,

        /// <summary>
        /// Treat superclass methods specially when invoked by the invokespecial instruction.
        /// </summary>
        ACC_SUPER = 0x0020,

        /// <summary>
        /// Is an interface, not a class.
        /// </summary>
        ACC_INTERFACE = 0x0200,

        /// <summary>
        /// Declared abstract; must not be instantiated.
        /// </summary>
        ACC_ABSTRACT = 0x0400,

        /// <summary>
        /// Declared synthetic; not present in the source code.
        /// </summary>
        ACC_SYNTHETIC = 0x1000,

        /// <summary>
        /// Declared as an annotation type.
        /// </summary>
        ACC_ANNOTATION = 0x2000,

        /// <summary>
        /// Declared as an enum type.
        /// </summary>
        ACC_ENUM = 0x4000
    }
}
