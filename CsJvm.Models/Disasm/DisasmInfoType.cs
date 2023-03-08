namespace CsJvm.Models.Disasm
{
    /// <summary>
    /// Info types
    /// </summary>
    public enum DisasmInfoType
    {
        /// <summary>
        /// Not used
        /// </summary>
        None,

        /// <summary>
        /// Primitive types (integer, boolean, floating point etc.)
        /// </summary>
        Primitive,

        /// <summary>
        /// Field info
        /// </summary>
        Field,

        /// <summary>
        /// Method info
        /// </summary>
        Method
    }
}
