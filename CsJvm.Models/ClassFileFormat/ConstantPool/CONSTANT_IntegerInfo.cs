namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// Кepresent 4-byte numeric integer constants
    /// </summary>
    public class CONSTANT_IntegerInfo : CpInfo
    {
        /// <summary>
        /// The bytes item of the CONSTANT_Integer_info structure represents the value of the int constant.
        /// The bytes of the value are stored in big-endian (high byte first) order.
        /// </summary>
        public uint Bytes { get; set; }

        /// <summary>
        /// Constant value
        /// </summary>
        public int Value => unchecked((int)Bytes);
    }
}
