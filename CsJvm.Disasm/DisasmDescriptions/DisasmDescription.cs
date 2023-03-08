namespace CsJvm.Disasm.DisasmDescriptions
{
    /// <summary>
    /// Provides disasm description info
    /// </summary>
    public static class DisasmDescription
    {
        /// <summary>
        /// Template method to get information
        /// </summary>
        /// <param name="descriptionsBase">Disasm description provider</param>
        /// <returns>String value</returns>
        public static string GetInfo(DisasmDescriptionsBase descriptionsBase)
            => descriptionsBase.GetInfo();
    }
}
