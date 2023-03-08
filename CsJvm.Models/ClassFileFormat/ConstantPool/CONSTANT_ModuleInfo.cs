namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// The structure used to represent a module
    /// </summary>
    public class CONSTANT_ModuleInfo : CpInfo
    {
        /// <summary>
        /// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure representing a valid module name.
        /// </summary>
        public ushort NameIndex { get; set; }
    }
}
