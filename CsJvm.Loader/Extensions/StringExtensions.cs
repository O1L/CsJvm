namespace CsJvm.Loader.Extensions
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Gets default field value by descriptor
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static object? GetDefaultValue(this string descriptor) =>
            descriptor[0] switch
            {
                // byte value
                'B' => (byte)0,

                // char (default value is the null code point)
                'C' => '\u0000',

                // double
                'D' => 0D,

                // float
                'F' => 0F,

                // int
                'I' => 0,

                // long
                'J' => 0L,

                // class reference
                'L' => null,

                // short
                'S' => (short)0,

                // boolean
                'Z' => false,

                // an array
                '[' => null,

                _ => throw new InvalidDataException($"Unknown field descriptor: {descriptor}")
            };
    }
}
