namespace CsJvm.Models.ClassFileFormat.Attributes
{
    /// <summary>
    /// Nested class access and property flags
    /// </summary>
    [Flags]
    public enum NestedClassAccessAndPropertyFlags
    {
        /// <summary>
        /// Marked or implicitly public in source.
        /// </summary>
        ACC_PUBLIC = 0x0001,

        /// <summary>
        /// Marked private in source.
        /// </summary>
        ACC_PRIVATE = 0x0002,

        /// <summary>
        /// Marked protected in source.
        /// </summary>
        ACC_PROTECTED = 0x0004,

        /// <summary>
        /// Marked or implicitly static in source.
        /// </summary>
        ACC_STATIC = 0x0008,

        /// <summary>
        /// Marked final in source.
        /// </summary>
        ACC_FINAL = 0x0010,

        /// <summary>
        /// Was an interface in source.
        /// </summary>
        ACC_INTERFACE = 0x0200,

        /// <summary>
        /// Marked or implicitly abstract in source.
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
