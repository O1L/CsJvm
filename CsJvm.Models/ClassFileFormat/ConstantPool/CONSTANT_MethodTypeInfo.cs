namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// The structure used to represent a method type
    /// </summary>
    public class CONSTANT_MethodTypeInfo : CpInfo
    {
        /// <summary>
        /// he constant_pool entry at that index must be a CONSTANT_Utf8_info structure representing a method descriptor (§4.3.3).
        /// </summary>
        public ushort DescriptorIndex { get; set; }
    }
}
