using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models;
using CsJvm.Models.ClassFileFormat.ConstantPool;

namespace CsJvm.VirtualMachine.Extensions
{
    /// <summary>
    /// Frame extensions
    /// </summary>
    public static class FrameExtensions
    {
        /// <summary>
        /// Gets new program counter
        /// </summary>
        /// <param name="frame">Frame</param>
        /// <param name="thread">Current thread</param>
        /// <returns>New PC value</returns>
        public static uint GetNewPC(this Frame frame, IJavaThread thread)
        {
            // -1 because ProgramCounter already updated
            var oldPC = thread.ProgramCounter - 1;

            var branchbyte1 = frame.Code[thread.ProgramCounter++];
            var branchbyte2 = frame.Code[thread.ProgramCounter++];

            var branchoffset = (short)((branchbyte1 << 8) | branchbyte2);
            var newPC = oldPC + branchoffset;

            return (uint)newPC;
        }

        /// <summary>
        /// Gets wide program counter
        /// </summary>
        /// <param name="frame">Frame</param>
        /// <param name="thread">Current thread</param>
        /// <returns>New PC value</returns>
        public static uint GetWidePC(this Frame frame, IJavaThread thread)
        {
            // -1 because ProgramCounter already updated
            var oldPC = thread.ProgramCounter - 1;

            var branchbyte1 = frame.Code[thread.ProgramCounter++];
            var branchbyte2 = frame.Code[thread.ProgramCounter++];
            var branchbyte3 = frame.Code[thread.ProgramCounter++];
            var branchbyte4 = frame.Code[thread.ProgramCounter++];

            var branchoffset = (branchbyte1 << 24) | (branchbyte2 << 16) | (branchbyte3 << 8) | branchbyte4;
            var newPC = oldPC + branchoffset;

            return (uint)newPC;
        }

        /// <summary>
        /// Ends current frame
        /// </summary>
        public static void EndCode(this Frame frame, IJavaThread thread)
        {
            thread.ProgramCounter = (uint)frame.Code.Length + 1;
            frame.OperandStack.Clear();
        }

        /// <summary>
        /// Creates a deep clone of current frame instance
        /// </summary>
        /// <param name="frame">Frame to clone</param>
        /// <returns>A new <see cref="Frame"/> clone</returns>
        public static Frame Clone(this Frame frame)
        {
            return new()
            {
                Code = (byte[])frame.Code.Clone(),
                CpInfo = (CpInfo[])frame.CpInfo.Clone(),
                LocalVariables = (object?[])frame.LocalVariables.Clone(),
                MethodName = frame.MethodName,
                OperandStack = new Stack<object?>(frame.OperandStack)
            };
        }
    }
}
