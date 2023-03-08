namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// The structure used to represent a method handle
    /// </summary>
    public class CONSTANT_MethodHandleInfo : CpInfo
    {
        /// <summary>
        /// The value of the reference_kind item must be in the range 1 to 9. 
        /// The value denotes the kind of this method handle, which characterizes its bytecode behavior.
        /// </summary>
        public byte ReferenceKind { get; set; }

        /// <summary>
        /// The constant_pool entry at that index must be as <see cref="BytecodeBehaviorsForMethodHandles"/>
        /// </summary>
        public ushort ReferenceIndex { get; set; }
    }
}
