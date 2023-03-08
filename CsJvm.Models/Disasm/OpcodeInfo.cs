namespace CsJvm.Models.Disasm
{
    /// <summary>
    /// Disassembled opcode info
    /// </summary>
    /// <param name="ProgramCounter">Program counter</param>
    /// <param name="Mnemonic">Opcode mnemonic</param>
    /// <param name="Index">Index value</param>
    /// <param name="BranchTarget">Branch target</param>
    /// <param name="Description">Calling methods / args description</param>
    public record struct OpcodeInfo(int ProgramCounter, string Mnemonic, int? Index, int? BranchTarget, string Description)
    {

        public string IndexBranch
        {
            get
            {
                var indexBranch = string.Empty;
                if (Index != null)
                    indexBranch = $"#{Index}";

                if (BranchTarget != null)
                    indexBranch = $"{BranchTarget}";

                return indexBranch;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var pc = $"{ProgramCounter}: ".PadLeft(6);
            var name = Mnemonic.PadRight(20);

            var indexBranch = string.Empty;
            if (Index != null)
                indexBranch = $"#{Index}";

            if (BranchTarget != null)
                indexBranch = $"{BranchTarget}";

            indexBranch = indexBranch.PadRight(10);

            return $"{pc}{name}{indexBranch}{Description}";
        }
    }
}
