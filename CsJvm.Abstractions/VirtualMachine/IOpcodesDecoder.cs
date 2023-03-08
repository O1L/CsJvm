namespace CsJvm.Abstractions.VirtualMachine
{
    /// <summary>
    /// Provides methods to call opcodes
    /// </summary>
    public interface IOpcodesDecoder
    {
        /// <summary>
        /// Executes single opcode
        /// </summary>
        /// <param name="thread">Thread to execute</param>
        void Execute(IJavaThread thread);
    }
}
