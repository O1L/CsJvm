namespace CsJvm.Models.Disasm
{
    /// <summary>
    /// Summary opcode data info
    /// </summary>
    /// <param name="Size">Opcode size (in bytes)</param>
    /// <param name="Opcode">Opcode signature</param>
    /// <param name="Name">Mnemonic</param>
    /// <param name="InfoType">Info type</param>
    /// <param name="IsBranch">If true, opcode is a branch instruction</param>
    public record struct OpcodeData(int Size, byte Opcode, string Name, DisasmInfoType InfoType = DisasmInfoType.None, bool IsBranch = false);
}
