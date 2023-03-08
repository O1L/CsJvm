namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// Fields structure
    /// </summary>
    public class CONSTANT_FieldrefInfo : CpInfo
    {
        /// <summary>
        /// A class type or an interface type
        /// </summary>
        public ushort ClassIndex { get; set; }

        /// <summary>
        /// A valid index into the constant_pool table
        /// </summary>
        public ushort NameAndTypeIndex { get; set; }
    }
}
