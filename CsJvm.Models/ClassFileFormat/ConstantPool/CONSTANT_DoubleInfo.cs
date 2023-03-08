namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// Represent 8-byte numeric double constants
    /// </summary>
    public class CONSTANT_DoubleInfo : CpInfo
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
        public double Value
        {
            get
            {
                var bits = ((long)HighBytes << 32) + LowBytes;

                if (bits == 0x7ff0000000000000L)
                    return double.PositiveInfinity;

                if ((ulong)bits == 0xfff0000000000000L)
                    return double.NegativeInfinity;

                if (bits > 0x7ff0000000000001L && bits < 0x7fffffffffffffffL || (ulong)bits > 0xfff0000000000001L && (ulong)bits < 0xffffffffffffffffL)
                    return double.NaN;

                // IEEE 754 binary64 floating-point format
                var s = ((bits >> 63) == 0) ? 1 : -1;
                var e = (int)((bits >> 52) & 0x7ffL);
                var m = (e == 0) ?
                           (bits & 0xfffffffffffffL) << 1 :
                           (bits & 0xfffffffffffffL) | 0x10000000000000L;

                return s * m * Math.Pow(2, e - 1075);
            }
        }
    }
}
