using System.Buffers.Binary;
using System.Text;

namespace CsJvm.Loader.Extensions
{
    /// <summary>
    /// <see cref="BinaryReader"/> extensions
    /// </summary>
    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// Reads 1 byte from reader
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <returns>Byte value</returns>
        public static byte ReadU1(this BinaryReader reader) =>
            reader.ReadByte();

        /// <summary>
        /// Reads 2 BE bytes as LE
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <returns>Ushort value</returns>
        public static ushort ReadU2(this BinaryReader reader) =>
            BinaryPrimitives.ReverseEndianness(reader.ReadUInt16());

        /// <summary>
        /// Reads 4 BE bytes as LE
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <returns>Uint value</returns>
        public static uint ReadU4(this BinaryReader reader) =>
            BinaryPrimitives.ReverseEndianness(reader.ReadUInt32());

        /// <summary>
        /// Reads string value from reader
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <param name="count">Bytes count to read</param>
        /// <returns>String value</returns>
        public static string ReadUtf8String(this BinaryReader reader, int count) =>
            Encoding.UTF8.GetString(reader.ReadBytes(count));
    }
}
