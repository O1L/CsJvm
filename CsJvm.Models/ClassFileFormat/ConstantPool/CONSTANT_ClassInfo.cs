namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// The CONSTANT_Class_info structure is used to represent a class or an interface
    /// </summary>
    public class CONSTANT_ClassInfo : CpInfo
    {
        /// <summary>
        /// The value of the name_index item must be a valid index into the constant_pool table. 
        /// </summary>
        public ushort NameIndex { get; set; }
    }
}
