namespace CsJvm.VirtualMachine.Extensions
{
    /// <summary>
    /// Numeric values extensions
    /// </summary>
    public static class NumericExtensions
    {
        /// <summary>
        /// Checks if value is a category 1 computational type
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="value">value ti check</param>
        /// <returns><see langword="true"/> if category 1; otherwise <see langword="false"/></returns>
        public static bool IsCategory1<T>(this T value) => value is not long && value is not double;
    }
}
