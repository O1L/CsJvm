namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// The CONSTANT_StringInfo structure is used to represent constant objects of the type String
    /// </summary>
    public class CONSTANT_StringInfo : CpInfo
    {
        /// <summary>
        /// A valid index into the constant_pool table, entry at that index must be a CONSTANT_Utf8_info structure 
        /// representing the sequence of Unicode code points to which the String object is to be initialized.
        /// </summary>
        public ushort StringIndex { get; set; }
    }
}
