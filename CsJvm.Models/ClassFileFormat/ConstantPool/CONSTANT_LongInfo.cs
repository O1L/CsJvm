namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// Represent 8-byte numeric long constants
    /// </summary>
    public class CONSTANT_LongInfo : CpInfo
    {
        /// <summary>
        /// High bytes
        /// </summary>
        public uint HighBytes { get; set; }

        /// <summary>
        /// Low bytes
        /// </summary>
        public uint LowBytes { get; set; }

        /// <summary>
        /// Constant value
        /// </summary>
        public long Value => ((long)HighBytes << 32) + LowBytes;
    }
}
