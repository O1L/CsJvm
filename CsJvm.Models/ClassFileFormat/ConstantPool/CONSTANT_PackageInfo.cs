namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// The structure used to represent a package exported or opened by a module
    /// </summary>
    public class CONSTANT_PackageInfo : CpInfo
    {
        /// <summary>
        /// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure representing a valid package name encoded in internal form.
        /// </summary>
        public ushort NameIndex { get; set; }
    }
}
