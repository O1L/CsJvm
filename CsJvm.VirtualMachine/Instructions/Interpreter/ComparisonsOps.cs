using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0x94, "lcmp")]
        public void Lcmp(IJavaThread thread)
        {
            var value2 = Convert.ToInt64(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt64(thread.CurrentMethod.OperandStack.Pop());

            var value = value1 > value2 ? 1
                      : value1 == value2 ? 0
                      : -1;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x95, "fcmpl")]
        public void Fcmpl(IJavaThread thread)
        {
            var value2 = Convert.ToSingle(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToSingle(thread.CurrentMethod.OperandStack.Pop());

            var value = value1 < value2 || float.IsNaN(value1) || float.IsNaN(value2) ? -1
                      : value1 == value2 ? 0
                      : 1;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x96, "fcmpg")]
        public void Fcmpg(IJavaThread thread)
        {
            var value2 = Convert.ToSingle(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToSingle(thread.CurrentMethod.OperandStack.Pop());

            var value = value1 > value2 || float.IsNaN(value1) || float.IsNaN(value2) ? 1
                      : value1 == value2 ? 0
                      : -1;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x97, "dcmpl")]
        public void Dcmpl(IJavaThread thread)
        {
            var value2 = Convert.ToDouble(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToDouble(thread.CurrentMethod.OperandStack.Pop());

            var value = value1 < value2 || double.IsNaN(value1) || double.IsNaN(value2) ? -1
                      : value1 == value2 ? 0
                      : 1;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x98, "dcmpg")]
        public void Dcmpg(IJavaThread thread)
        {
            var value2 = Convert.ToDouble(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToDouble(thread.CurrentMethod.OperandStack.Pop());

            var value = value1 > value2 || double.IsNaN(value1) || double.IsNaN(value2) ? 1
                      : value1 == value2 ? 0
                      : -1;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x99, "ifeq")]
        public void Ifeq(IJavaThread thread)
        {
            var value = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            IfCmp(value == 0, thread);
        }

        [Opcode(0x9a, "ifne")]
        public void Ifne(IJavaThread thread)
        {
            var value = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            IfCmp(value != 0, thread);
        }

        [Opcode(0x9b, "iflt")]
        public void Iflt(IJavaThread thread)
        {
            var value = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            IfCmp(value < 0, thread);
        }

        [Opcode(0x9c, "ifge")]
        public void Ifge(IJavaThread thread)
        {
            var value = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            IfCmp(value >= 0, thread);
        }

        [Opcode(0x9d, "ifgt")]
        public void Ifgt(IJavaThread thread)
        {
            var value = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            IfCmp(value > 0, thread);
        }

        [Opcode(0x9e, "ifle")]
        public void Ifle(IJavaThread thread)
        {
            var value = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            IfCmp(value <= 0, thread);
        }

        [Opcode(0x9f, "if_icmpeq")]

        public void FfIcmpeq(IJavaThread thread)
        {
            var value2 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            IfCmp(value1 == value2, thread);
        }

        [Opcode(0xa0, "if_icmpne")]
        public void FfIcmpne(IJavaThread thread)
        {
            var value2 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            IfCmp(value1 != value2, thread);
        }

        [Opcode(0xa1, "if_icmplt")]
        public void FfIcmplt(IJavaThread thread)
        {
            var value2 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            IfCmp(value1 < value2, thread);
        }

        [Opcode(0xa2, "if_icmpge")]
        public void FfIcmpge(IJavaThread thread)
        {
            var value2 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            IfCmp(value1 >= value2, thread);
        }

        [Opcode(0xa3, "if_icmpgt")]
        public void FfIcmpgt(IJavaThread thread)
        {
            var value2 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            IfCmp(value1 > value2, thread);
        }

        [Opcode(0xa4, "if_icmple")]
        public void FfIcmple(IJavaThread thread)
        {
            var value2 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            var value1 = Convert.ToInt32(thread.CurrentMethod.OperandStack.Pop());
            IfCmp(value1 <= value2, thread);
        }

        [Opcode(0xa5, "if_acmpeq")]
        public void IfAcmpeq(IJavaThread thread)
        {
            var value2 = thread.CurrentMethod.OperandStack.Pop();
            var value1 = thread.CurrentMethod.OperandStack.Pop();

            IfCmp(value1!.Equals(value2), thread);
        }

        [Opcode(0xa6, "if_acmpne")]
        public void IfAcmpne(IJavaThread thread)
        {
            var value2 = thread.CurrentMethod.OperandStack.Pop();
            var value1 = thread.CurrentMethod.OperandStack.Pop();
            IfCmp(!value1!.Equals(value2), thread);
        }

        private static void IfCmp(bool cond, IJavaThread thread)
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
        }
    }
}
