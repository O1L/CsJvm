using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;
using CsJvm.VirtualMachine.Extensions;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{

    public partial class JvmInterpreter
    {
        [Opcode(0xa7, "goto")]
        public Task Goto(IJavaThread thread)
        {
            var newPC = thread.CurrentMethod.GetNewPC(thread);
            thread.ProgramCounter = newPC;
            return Task.CompletedTask;
        }

        [Opcode(0xa8, "jsr")]
        public Task Jsr(IJavaThread thread)
        {
            var newPC = thread.CurrentMethod.GetNewPC(thread);
            thread.CurrentMethod.OperandStack.Push(newPC);
            return Task.CompletedTask;
        }

        [Opcode(0xa9, "ret")]
        public Task Ret(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var newPC = thread.CurrentMethod.LocalVariables[index];
            thread.ProgramCounter = (uint)newPC!;
            return Task.CompletedTask;
        }

        [Opcode(0xaa, "tableswitch")]
        public Task TableSwitch(IJavaThread thread) => throw new NotImplementedException("tableswitch");

        [Opcode(0xab, "lookupswitch")]
        public Task LookupSwitch(IJavaThread thread) => throw new NotImplementedException("lookupswitch");

        [Opcode(0xac, "ireturn")]
        public Task Ireturn(IJavaThread thread) => Return<int>(thread);

        [Opcode(0xad, "lreturn")]
        public Task Lreturn(IJavaThread thread) => Return<long>(thread);

        [Opcode(0xae, "freturn")]
        public Task Freturn(IJavaThread thread) => Return<float>(thread);

        [Opcode(0xaf, "dreturn")]
        public Task Dreturn(IJavaThread thread) => Return<double>(thread);

        [Opcode(0xb0, "areturn")]
        public Task Areturn(IJavaThread thread)
        {
            var value = thread.CurrentMethod.OperandStack.Pop();
            thread.PreviousMethod.OperandStack.Push(value);
            thread.CurrentMethod.EndCode(thread);
            return Task.CompletedTask;
        }

        [Opcode(0xb1, "return")]
        public Task Return(IJavaThread thread)
        {
            thread.CurrentMethod.EndCode(thread);
            return Task.CompletedTask;
        }

        private static Task Return<T>(IJavaThread thread)
        {
            var value = thread.CurrentMethod.OperandStack.Pop();
            if (value is not T)
                throw new InvalidOperationException($"Bad data type: {typeof(T)}");

            thread.PreviousMethod.OperandStack.Push(value);
            thread.CurrentMethod.EndCode(thread);

            return Task.CompletedTask;
        }
    }
}
