namespace CsJvm.VirtualMachine.Extensions
{
    /// <summary>
    /// Stack extensions
    /// </summary>
    public static class StackExtensions
    {
        /// <summary>
        /// Pop all values from stack
        /// </summary>
        /// <param name="stack">Stack to pop</param>
        /// <returns>Array of values</returns>
        public static object[] PopAll(this Stack<object> stack)
        {
            var values = stack.ToArray();
            Array.Reverse(values);
            stack.Clear();
            return values;
        }

        /// <summary>
        /// Pop specified number of values from stack
        /// </summary>
        /// <param name="stack">Stack to pop</param>
        /// <param name="count">Count to pop</param>
        /// <returns>Array of values</returns>
        public static object?[] PopCountOf(this Stack<object?> stack, int count)
        {
            if (count < 0)
                throw new ArgumentException("Count must be >= 0!");

            var args = new List<object?>();
            while (count-- > 0)
            {
                var value = stack.Pop();
                if (value is long || value is double)
                    count--;

                args.Add(value);
            }

            args.Reverse();
            return args.ToArray()!;
        }
    }
}
