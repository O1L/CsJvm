namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// The structure used to represent a field or method
    /// </summary>
    public class CONSTANT_NameAndTypeInfo : CpInfo
    {
        /// <summary>
        /// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure representing either 
        /// a valid unqualified name denoting a field or method, or the special method name &lt;init&gt;.
        /// </summary>
        public ushort NameIndex { get; set; }

        /// <summary>
        /// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure representing 
        /// a valid field descriptor or method descriptor.
        /// </summary>
        public ushort DescriptorIndex { get; set; }
    }
}
