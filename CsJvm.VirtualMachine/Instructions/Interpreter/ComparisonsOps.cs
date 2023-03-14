using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0x94, "lcmp")]
        public Task Lcmp(IJavaThread thread)
        {
            var value2 = Convert.ToInt64(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt64(thread.CurrentMethod.OperandStack.Pop());

            var value = value1 > value2 ? 1
                      : value1 == value2 ? 0
                      : -1;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x95, "fcmpl")]
        public Task Fcmpl(IJavaThread thread)
        {
            var value2 = Convert.ToSingle(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToSingle(thread.CurrentMethod.OperandStack.Pop());

            var value = value1 < value2 || float.IsNaN(value1) || float.IsNaN(value2) ? -1
                      : value1 == value2 ? 0
                      : 1;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x96, "fcmpg")]
        public Task Fcmpg(IJavaThread thread)
        {
            var value2 = Convert.ToSingle(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToSingle(thread.CurrentMethod.OperandStack.Pop());

            var value = value1 > value2 || float.IsNaN(value1) || float.IsNaN(value2) ? 1
                      : value1 == value2 ? 0
                      : -1;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x97, "dcmpl")]
        public Task Dcmpl(IJavaThread thread)
        {
            var value2 = Convert.ToDouble(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToDouble(thread.CurrentMethod.OperandStack.Pop());

            var value = value1 < value2 || double.IsNaN(value1) || double.IsNaN(value2) ? -1
                      : value1 == value2 ? 0
                      : 1;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x98, "dcmpg")]
        public Task Dcmpg(IJavaThread thread)
        {
            var value2 = Convert.ToDouble(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToDouble(thread.CurrentMethod.OperandStack.Pop());

            var value = value1 > value2 || double.IsNaN(value1) || double.IsNaN(value2) ? 1
                      : value1 == value2 ? 0
                      : -1;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x99, "ifeq")]
        public Task Ifeq(IJavaThread thread)
        {
            var value = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            return IfCmp(value == 0, thread);
        }

        [Opcode(0x9a, "ifne")]
        public Task Ifne(IJavaThread thread)
        {
            var value = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            return IfCmp(value != 0, thread);
        }

        [Opcode(0x9b, "iflt")]
        public Task Iflt(IJavaThread thread)
        {
            var value = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            return IfCmp(value < 0, thread);
        }

        [Opcode(0x9c, "ifge")]
        public Task Ifge(IJavaThread thread)
        {
            var value = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            return IfCmp(value >= 0, thread);
        }

        [Opcode(0x9d, "ifgt")]
        public Task Ifgt(IJavaThread thread)
        {
            var value = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            return IfCmp(value > 0, thread);
        }

        [Opcode(0x9e, "ifle")]
        public Task Ifle(IJavaThread thread)
        {
            var value = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            return IfCmp(value <= 0, thread);
        }

        [Opcode(0x9f, "if_icmpeq")]

        public Task FfIcmpeq(IJavaThread thread)
        {
            var value2 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            return IfCmp(value1 == value2, thread);
        }

        [Opcode(0xa0, "if_icmpne")]
        public Task FfIcmpne(IJavaThread thread)
        {
            var value2 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            return IfCmp(value1 != value2, thread);
        }

        [Opcode(0xa1, "if_icmplt")]
        public Task FfIcmplt(IJavaThread thread)
        {
            var value2 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            return IfCmp(value1 < value2, thread);
        }

        [Opcode(0xa2, "if_icmpge")]
        public Task FfIcmpge(IJavaThread thread)
        {
            var value2 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            return IfCmp(value1 >= value2, thread);
        }

        [Opcode(0xa3, "if_icmpgt")]
        public Task FfIcmpgt(IJavaThread thread)
        {
            var value2 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            return IfCmp(value1 > value2, thread);
        }

        [Opcode(0xa4, "if_icmple")]
        public Task FfIcmple(IJavaThread thread)
        {
            var value2 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            return IfCmp(value1 <= value2, thread);
        }

        [Opcode(0xa5, "if_acmpeq")]
        public Task IfAcmpeq(IJavaThread thread)
        {
            var value2 = thread.CurrentMethod.OperandStack.Pop();
            var value1 = thread.CurrentMethod.OperandStack.Pop();

            return IfCmp(value1!.Equals(value2), thread);
        }

        [Opcode(0xa6, "if_acmpne")]
        public Task IfAcmpne(IJavaThread thread)
        {
            var value2 = thread.CurrentMethod.OperandStack.Pop();
            var value1 = thread.CurrentMethod.OperandStack.Pop();
            return IfCmp(!value1!.Equals(value2), thread);
        }

        private static Task IfCmp(bool cond, IJavaThread thread)
        {
            // -1 because ProgramCounter already updated
            var oldPC = thread.ProgramCounter - 1;

            var branchbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var branchbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];

            // construct new code target
            var branchbyte = (short)((branchbyte1 << 8) | branchbyte2);
            var newPC = oldPC + branchbyte;

            // set new PC
            if (cond)
                thread.ProgramCounter = (uint)newPC;

            return Task.CompletedTask;
        }
    }
}
