using System.Text;

namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    /// <summary>
    /// The structure used to represent constant string values
    /// </summary>
    public class CONSTANT_Utf8Info : CpInfo
    {
        /// <summary>
        /// The value of the length item gives the number of bytes in the bytes array (not the length of the resulting string).
        /// </summary>
        public ushort Length { get; set; }

        /// <summary>
        /// The bytes array contains the bytes of the string.
        /// <para>No byte may have the value (byte)0.</para>
        /// <para>No byte may lie in the range (byte)0xf0 to (byte)0xff.</para>
        /// </summary>
        public byte[] Bytes { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Gets UTF-8 string-representation of bytes
        /// </summary>
        public string Utf8String => Encoding.UTF8.GetString(Bytes);
    }
}
