namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// The structure  used to represent a dynamically-computed call site
    /// </summary>
    public class CONSTANT_InvokeDynamicInfo : CpInfo
    {
        /// <summary>
        /// The value of this index item must be a valid index into the bootstrap_methods array of the bootstrap method table of this class file.
        /// </summary>
        public ushort BootstrapMethodAttrIndex { get; set; }

        /// <summary>
        /// The constant_pool entry at that index must be a CONSTANT_NameAndType_info structure. This constant_pool entry indicates a name and descriptor.
        /// </summary>
        public ushort NameAndTypeIndex { get; set; }
    }
}
