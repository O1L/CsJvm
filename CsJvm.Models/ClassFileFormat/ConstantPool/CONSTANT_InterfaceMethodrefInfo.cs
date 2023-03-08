namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// Interface methods structure
    /// </summary>
    public class CONSTANT_InterfaceMethodrefInfo : CpInfo
    {
        /// <summary>
        /// An interface type, not a class type
        /// </summary>
        public ushort ClassIndex { get; set; }

        /// <summary>
        /// A valid index into the constant_pool table
        /// </summary>
        public ushort NameAndTypeIndex { get; set; }
    }
}
