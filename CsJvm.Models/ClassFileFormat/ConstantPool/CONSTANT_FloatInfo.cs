namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// Кepresent 4-byte numeric float constants
    /// </summary>
    public class CONSTANT_FloatInfo : CpInfo
    {
        /// <summary>
        /// The value of the float constant in IEEE 754 binary32 floating-point format.
        /// The bytes of the item are stored in big-endian (high byte first) order.
        /// </summary>
        public uint Bytes { get; set; }

        /// <summary>
        /// Constant value
        /// </summary>
        public float Value
        {
            get
            {
                if (Bytes == 0x7f800000)
                    return float.PositiveInfinity;

                if ((ulong)Bytes == 0xff800000)
                    return float.NegativeInfinity;

                if (Bytes > 0x7f800001u && Bytes < 0x7fffffffu || Bytes > 0xff800001u && Bytes < 0xffffffffu)
                    return float.NaN;

                // IEEE 754 binary64 floating-point format
                var s = ((Bytes >> 31) == 0) ? 1 : -1;
                var e = (int)((Bytes >> 23) & 0xff);
                var m = (e == 0) ?
                           (Bytes & 0x7fffff) << 1 :
                           (Bytes & 0x7fffff) | 0x800000;

                return s * m * MathF.Pow(2, e - 150);
            }
        }
    }
}
