using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;
using CsJvm.VirtualMachine.Extensions;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0x57, "pop")]
        public Task Pop(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Pop();
            return Task.CompletedTask;
        }

        [Opcode(0x58, "pop2")]
        public Task Pop2(IJavaThread thread)
        {
            // remove first value
            thread.CurrentMethod.OperandStack.Pop();

            // if it is a category 1, remove it too
            var value = thread.CurrentMethod.OperandStack.Peek();
            if (value is not long && value is not double)
                thread.CurrentMethod.OperandStack.Pop();

            return Task.CompletedTask;
        }

        [Opcode(0x59, "dup")]
        public Task Dup(IJavaThread thread)
        {
            var value = thread.CurrentMethod.OperandStack.Peek();
            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x5a, "dup_x1")]
        public Task DupX1(IJavaThread thread)
        {
            var value1 = thread.CurrentMethod.OperandStack.Pop();
            var value2 = thread.CurrentMethod.OperandStack.Pop();

            thread.CurrentMethod.OperandStack.Push(value1);
            thread.CurrentMethod.OperandStack.Push(value2);
            thread.CurrentMethod.OperandStack.Push(value1);

            return Task.CompletedTask;
        }

        [Opcode(0x5b, "dup_x2")]
        public Task DupX2(IJavaThread thread) => throw new NotImplementedException();

        [Opcode(0x5c, "dup2")]
        public Task Dup2(IJavaThread thread)
        {
            // Form 2
            if (!thread.CurrentMethod.OperandStack.Peek().IsCategory1())
            {
                var value = thread.CurrentMethod.OperandStack.Peek();
                thread.CurrentMethod.OperandStack.Push(value);
                return Task.CompletedTask;
            }

            // Form 1
            var value1 = thread.CurrentMethod.OperandStack.Pop();
            var value2 = thread.CurrentMethod.OperandStack.Pop();

            thread.CurrentMethod.OperandStack.Push(value2);
            thread.CurrentMethod.OperandStack.Push(value1);
            thread.CurrentMethod.OperandStack.Push(value2);
            thread.CurrentMethod.OperandStack.Push(value1);
            return Task.CompletedTask;
        }

        [Opcode(0x5d, "dup2_x1")]
        public Task Dup2X1(IJavaThread thread)
        {
            var value1 = thread.CurrentMethod.OperandStack.Pop();
            var value2 = thread.CurrentMethod.OperandStack.Pop();

            // Form 2
            if (!value1.IsCategory1() && !value2.IsCategory1())
            {
                thread.CurrentMethod.OperandStack.Push(value1);
                thread.CurrentMethod.OperandStack.Push(value2);
                thread.CurrentMethod.OperandStack.Push(value1);
                return Task.CompletedTask;
            }

            // Form 1
            var value3 = thread.CurrentMethod.OperandStack.Pop();
            thread.CurrentMethod.OperandStack.Push(value2);
            thread.CurrentMethod.OperandStack.Push(value1);
            thread.CurrentMethod.OperandStack.Push(value3);
            thread.CurrentMethod.OperandStack.Push(value2);
            thread.CurrentMethod.OperandStack.Push(value1);
            return Task.CompletedTask;
        }

        [Opcode(0x5e, "dup2_x2")]
        public Task Dup2X2(IJavaThread thread) => throw new NotImplementedException();

        [Opcode(0x5f, "swap")]
        public Task Swap(IJavaThread thread)
        {
            var value1 = thread.CurrentMethod.OperandStack.Pop();
            var value2 = thread.CurrentMethod.OperandStack.Pop();

            thread.CurrentMethod.OperandStack.Push(value1);
            thread.CurrentMethod.OperandStack.Push(value2);
            return Task.CompletedTask;
        }
    }
}
