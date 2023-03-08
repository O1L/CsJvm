using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;
using CsJvm.VirtualMachine.Extensions;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{

    public partial class JvmInterpreter
    {
        [Opcode(0xa7, "goto")]
        public void Goto(IJavaThread thread)
        {
            var newPC = thread.CurrentMethod.GetNewPC(thread);
            thread.ProgramCounter = newPC;
        }

        [Opcode(0xa8, "jsr")]
        public void Jsr(IJavaThread thread)
        {
            var newPC = thread.CurrentMethod.GetNewPC(thread);
            thread.CurrentMethod.OperandStack.Push(newPC);
        }

        [Opcode(0xa9, "ret")]
        public void Ret(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var newPC = thread.CurrentMethod.LocalVariables[index];
            thread.ProgramCounter = (uint)newPC!;
        }

        [Opcode(0xaa, "tableswitch")]
        public void TableSwitch(IJavaThread thread) => throw new NotImplementedException("tableswitch");

        [Opcode(0xab, "lookupswitch")]
        public void LookupSwitch(IJavaThread thread) => throw new NotImplementedException("lookupswitch");

        [Opcode(0xac, "ireturn")]
        public void Ireturn(IJavaThread thread) => Return<int>(thread);

        [Opcode(0xad, "lreturn")]
        public void Lreturn(IJavaThread thread) => Return<long>(thread);

        [Opcode(0xae, "freturn")]
        public void Freturn(IJavaThread thread) => Return<float>(thread);

        [Opcode(0xaf, "dreturn")]
        public void Dreturn(IJavaThread thread) => Return<double>(thread);

        [Opcode(0xb0, "areturn")]
        public void Areturn(IJavaThread thread)
        {
            var value = thread.CurrentMethod.OperandStack.Pop();
            thread.PreviousMethod.OperandStack.Push(value);
            thread.CurrentMethod.EndCode(thread);
        }

        [Opcode(0xb1, "return")]
        public void Return(IJavaThread thread)
            => thread.CurrentMethod.EndCode(thread);

        private static void Return<T>(IJavaThread thread)
        {
            var value = thread.CurrentMethod.OperandStack.Pop();
            if (value is not T)
                throw new InvalidOperationException($"Bad data type: {typeof(T)}");

            thread.PreviousMethod.OperandStack.Push(value);
            thread.CurrentMethod.EndCode(thread);
        }
    }
}
