using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0x60, "iadd")]
        public Task Iadd(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 + value2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x61, "ladd")]
        public Task Ladd(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 + value2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x62, "fadd")]
        public Task Fadd(IJavaThread thread)
        {
            var value2 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 + value2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x63, "dadd")]
        public Task Dadd(IJavaThread thread)
        {
            var value2 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 + value2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x64, "isub")]
        public Task Isub(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 - value2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x65, "lsub")]
        public Task Lsub(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 - value2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x66, "fsub")]
        public Task Fsub(IJavaThread thread)
        {
            var value2 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 - value2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x67, "dsub")]
        public Task Dsub(IJavaThread thread)
        {
            var value2 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 - value2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x68, "imul")]
        public Task Imul(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 * value2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x69, "lmul")]
        public Task Lmul(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 * value2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x6a, "fmul")]
        public Task Fmul(IJavaThread thread)
        {
            var value2 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 * value2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x6b, "dmul")]
        public Task Dmul(IJavaThread thread)
        {
            var value2 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value = value1 * value2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x6c, "idiv")]
        public Task Idiv(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 / value2;
            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x6d, "ldiv")]
        public Task Ldiv(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 / value2;
            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x6e, "fdiv")]
        public Task Fdiv(IJavaThread thread)
        {
            var value2 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (float?)thread.CurrentMethod.OperandStack.Pop();
            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 / value2;
            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x6f, "ddiv")]
        public Task Ddiv(IJavaThread thread)
        {
            var value2 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (double?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 / value2;
            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x70, "irem")]
        public Task Irem(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 % value2;
            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x71, "lrem")]
        public Task Lrem(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 % value2;
            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x72, "frem")]
        public Task Frem(IJavaThread thread)
        {
            var value2 = (float?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (float?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 % value2;
            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x73, "drem")]
        public Task Drem(IJavaThread thread)
        {
            var value2 = (double?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (double?)thread.CurrentMethod.OperandStack.Pop();

            if (value2 == 0)
                throw new ArithmeticException();

            var value = value1 % value2;
            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x74, "ineg")]
        public Task Ineg(IJavaThread thread)
        {
            var value = (int?)thread.CurrentMethod.OperandStack.Pop();

            var val = (~value) + 1;

            thread.CurrentMethod.OperandStack.Push(-value);
            return Task.CompletedTask;
        }

        [Opcode(0x75, "lneg")]
        public Task Lneg(IJavaThread thread)
        {
            var value = (long?)thread.CurrentMethod.OperandStack.Pop();
            thread.CurrentMethod.OperandStack.Push(-value);
            return Task.CompletedTask;
        }

        [Opcode(0x76, "fneg")]
        public Task Fneg(IJavaThread thread)
        {
            var value = (float?)thread.CurrentMethod.OperandStack.Pop();
            thread.CurrentMethod.OperandStack.Push(-value);
            return Task.CompletedTask;
        }

        [Opcode(0x77, "dneg")]
        public Task Dneg(IJavaThread thread)
        {
            var value = (double?)thread.CurrentMethod.OperandStack.Pop();
            thread.CurrentMethod.OperandStack.Push(-value);
            return Task.CompletedTask;
        }

        [Opcode(0x78, "ishl")]
        public Task Ishl(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop() & 0b11111;
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 << value2;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x79, "lshl")]
        public Task Lshl(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop() & 0b111111;
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 << value2;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x7a, "ishr")]
        public Task Ishr(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop() & 0b11111;
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 >> value2;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x7b, "lshr")]
        public Task Lshr(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop() & 0b111111;
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 >> value2;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x7c, "iushr")]
        public Task Iushr(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop() & 0b111111;
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 >> value2;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x7d, "lushr")]
        public Task Lushr(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop() & 0b111111;
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 >> value2;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x7e, "iand")]
        public Task Iand(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 & value2;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x7f, "land")]
        public Task Land(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 & value2;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x80, "ior")]
        public Task Ior(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 | value2;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x81, "lor")]
        public Task Lor(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 | value2;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x82, "ixor")]
        public Task Ixor(IJavaThread thread)
        {
            var value2 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (int?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 ^ value2;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x83, "lxor")]
        public Task Lxor(IJavaThread thread)
        {
            var value2 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var value1 = (long?)thread.CurrentMethod.OperandStack.Pop();
            var result = value1 ^ value2;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x84, "iinc")]
        public Task Iinc(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++] & 0xff;
            var constImm = unchecked((sbyte)thread.CurrentMethod.Code[thread.ProgramCounter++]);

            thread.CurrentMethod.LocalVariables[index] = (int)thread.CurrentMethod.LocalVariables[index]! + constImm;
            return Task.CompletedTask;
        }
    }
}
