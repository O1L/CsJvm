namespace CsJvm.Models.ClassFileFormat.Fields
{
    /// <summary>
    /// Field access and property flags
    /// </summary>
    [Flags]
    public enum FieldAccessAndPropertyFlags
    {
        /// <summary>
        /// Declared public; may be accessed from outside its package.
        /// </summary>
        ACC_PUBLIC = 0x0001,

        /// <summary>
        /// Declared private; usable only within the defining class.
        /// </summary>
        ACC_PRIVATE = 0x0002,

        /// <summary>
        /// Declared protected; may be accessed within subclasses.
        /// </summary>
        ACC_PROTECTED = 0x0004,

        /// <summary>
        /// Declared static.
        /// </summary>
        ACC_STATIC = 0x0008,

        /// <summary>
        /// Declared final; never directly assigned to after object construction.
        /// </summary>
        ACC_FINAL = 0x0010,

        /// <summary>
        /// Declared volatile; cannot be cached.
        /// </summary>
        ACC_VOLATILE = 0x0040,

        /// <summary>
        /// Declared transient; not written or read by a persistent object manager.
        /// </summary>
        ACC_TRANSIENT = 0x0080,

        /// <summary>
        /// Declared synthetic; not present in the source code.
        /// </summary>
        ACC_SYNTHETIC = 0x1000,

        /// <summary>
        /// Declared as an element of an enum.
        /// </summary>
        ACC_ENUM = 0x4000
    }
}
