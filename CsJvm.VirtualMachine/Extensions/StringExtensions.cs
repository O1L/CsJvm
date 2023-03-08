namespace CsJvm.VirtualMachine.Extensions
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns arguments count from method descriptor
        /// </summary>
        /// <param name="descriptor">Method descriptor</param>
        /// <returns>integer value</returns>
        public static int GetArgsCount(this string descriptor)
        {
            // remove first '(' char
            if (descriptor[0] == '(')
                descriptor = descriptor[1..];

            // recursive search and parsing
            var symbol = descriptor[0];

            return symbol switch
            {
                ')' => 0,
                'B' or 'C' or 'F' or 'I' or 'S' or 'Z' => 1 + descriptor[1..].GetArgsCount(),
                'J' or 'D' => 2 + descriptor[1..].GetArgsCount(),
                'L' => 1 + descriptor[(descriptor.IndexOf(';') + 1)..].GetArgsCount(),
                '[' => descriptor[1..].GetArgsCount(),

                _ => throw new ArgumentException($"Bad argument type in '{descriptor}': '{symbol}'")
            };
        }
    }
}
