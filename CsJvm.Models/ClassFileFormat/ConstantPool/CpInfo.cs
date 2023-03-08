namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// The Constant Pool
    /// </summary>
    public abstract class CpInfo
    {
        /// <summary>
        /// A 1-byte tag indicating the kind of constant denoted by the entry.
        /// </summary>
        public ConstantPoolTag Tag { get; set; }
    }
}
