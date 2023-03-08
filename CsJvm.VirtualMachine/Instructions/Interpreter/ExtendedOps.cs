using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;
using CsJvm.VirtualMachine.Extensions;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0xc4, "wide")]
        public void Wide(IJavaThread thread) => throw new NotImplementedException();

        [Opcode(0xc5, "multianewarray")]
        public void MultiAnewArray(IJavaThread thread) => throw new NotImplementedException();

        [Opcode(0xc6, "ifnull")]
        public void IfNull(IJavaThread thread)
        {
            var value = thread.CurrentMethod.OperandStack.Pop();
            var newPC = thread.CurrentMethod.GetNewPC(thread);

            if (value == null)
                thread.ProgramCounter = newPC;
        }

        [Opcode(0xc7, "ifnonnull")]
        public void IfNonNull(IJavaThread thread)
        {
            var value = thread.CurrentMethod.OperandStack.Pop();
            var newPC = thread.CurrentMethod.GetNewPC(thread);

            if (value != null)
                thread.ProgramCounter = newPC;
        }

        [Opcode(0xc8, "goto_w")]
        public void GotoW(IJavaThread thread)
        {
            var newPC = thread.CurrentMethod.GetWidePC(thread);
            thread.ProgramCounter = newPC;
        }

        [Opcode(0xc9, "jsr_w")]
        public void JsrW(IJavaThread thread)
        {
            var newPC = thread.CurrentMethod.GetWidePC(thread);
            thread.CurrentMethod.OperandStack.Push(newPC);
        }
    }
}
