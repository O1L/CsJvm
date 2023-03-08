namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// Methods structure
    /// </summary>
    public class CONSTANT_MethodrefInfo : CpInfo
    {
        /// <summary>
        /// A class type, not an interface type
        /// </summary>
        public ushort Classindex { get; set; }

        /// <summary>
        /// A valid index into the constant_pool table
        /// </summary>
        public ushort NameAndTypeIndex { get; set; }
    }
}
