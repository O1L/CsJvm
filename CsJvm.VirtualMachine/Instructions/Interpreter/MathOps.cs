using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0x60, "iadd")]
        public void Iadd(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 + value2;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x61, "ladd")]
        public void Ladd(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 + value2;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x62, "fadd")]
        public void Fadd(IJavaThread thread)
        {
            var value2 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 + value2;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x63, "dadd")]
        public void Dadd(IJavaThread thread)
        {
            var value2 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 + value2;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x64, "isub")]
        public void Isub(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 - value2;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x65, "lsub")]
        public void Lsub(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 - value2;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x66, "fsub")]
        public void Fsub(IJavaThread thread)
        {
            var value2 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 - value2;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x67, "dsub")]
        public void Dsub(IJavaThread thread)
        {
            var value2 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 - value2;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x68, "imul")]
        public void Imul(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 * value2;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x69, "lmul")]
        public void Lmul(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 * value2;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x6a, "fmul")]
        public void Fmul(IJavaThread thread)
        {
            var value2 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 * value2;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x6b, "dmul")]
        public void Dmul(IJavaThread thread)
        {
            var value2 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 * value2;

            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x6c, "idiv")]
        public void Idiv(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 / value2;
            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x6d, "ldiv")]
        public void Ldiv(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 / value2;
            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x6e, "fdiv")]
        public void Fdiv(IJavaThread thread)
        {
            var value2 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (float?)thread.CurrentMethod.OperandStack.Pop();
            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 / value2;
            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x6f, "ddiv")]
        public void Ddiv(IJavaThread thread)
        {
            var value2 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (double?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 / value2;
            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x70, "irem")]
        public void Irem(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 % value2;
            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x71, "lrem")]
        public void Lrem(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 % value2;
            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x72, "frem")]
        public void Frem(IJavaThread thread)
        {
            var value2 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (float?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 % value2;
            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x73, "drem")]
        public void Drem(IJavaThread thread)
        {
            var value2 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (double?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 % value2;
            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0x74, "ineg")]
        public void Ineg(IJavaThread thread)
        {
            var value = (int?)thread.CurrentMethod.OperandStack.Pop();

            var val = (~value) + 1;

            thread.CurrentMethod.OperandStack.Push(-value);
        }

        [Opcode(0x75, "lneg")]
        public void Lneg(IJavaThread thread)
        {
            var value = (long?)thread.CurrentMethod.OperandStack.Pop();
            thread.CurrentMethod.OperandStack.Push(-value);
        }

        [Opcode(0x76, "fneg")]
        public void Fneg(IJavaThread thread)
        {
            var value = (float?)thread.CurrentMethod.OperandStack.Pop();
            thread.CurrentMethod.OperandStack.Push(-value);
        }

        [Opcode(0x77, "dneg")]
        public void Dneg(IJavaThread thread)
        {
            var value = (double?)thread.CurrentMethod.OperandStack.Pop();
            thread.CurrentMethod.OperandStack.Push(-value);
        }

        [Opcode(0x78, "ishl")]
        public void Ishl(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop() & 0b11111;
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 << value2;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x79, "lshl")]
        public void Lshl(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop() & 0b111111;
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 << value2;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x7a, "ishr")]
        public void Ishr(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop() & 0b11111;
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 >> value2;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x7b, "lshr")]
        public void Lshr(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop() & 0b111111;
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 >> value2;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x7c, "iushr")]
        public void Iushr(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop() & 0b111111;
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 >> value2;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x7d, "lushr")]
        public void Lushr(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop() & 0b111111;
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 >> value2;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x7e, "iand")]
        public void Iand(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 & value2;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x7f, "land")]
        public void Land(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 & value2;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x80, "ior")]
        public void Ior(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 | value2;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x81, "lor")]
        public void Lor(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 | value2;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x82, "ixor")]
        public void Ixor(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 ^ value2;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x83, "lxor")]
        public void Lxor(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 ^ value2;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x84, "iinc")]
        public void Iinc(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++] & 0xff;
            var constImm = unchecked((sbyte)thread.CurrentMethod.Code[thread.ProgramCounter++]);

            thread.CurrentMethod.LocalVariables[index] = (int)thread.CurrentMethod.LocalVariables[index]! + constImm;
        }
    }
}
