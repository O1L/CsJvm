using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models.ClassFileFormat;
using CsJvm.Models.Heap;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0x15, "iload")]
        public Task Iload(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            return Load<int>(thread, index);
        }

        [Opcode(0x16, "lload")]
        public Task Lload(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            return Load<long>(thread, index);
        }

        [Opcode(0x17, "fload")]
        public Task Fload(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            return Load<float>(thread, index);
        }

        [Opcode(0x18, "dload")]
        public Task Dload(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            return Load<double>(thread, index);
        }

        [Opcode(0x19, "aload")]
        public Task Aload(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            thread.CurrentMethod.OperandStack.Push(thread.CurrentMethod.LocalVariables[index]);
            return Task.CompletedTask;
        }

        [Opcode(0x1a, "iload_0")]
        public Task Iload0(IJavaThread thread) => Load<int>(thread, 0);

        [Opcode(0x1b, "iload_1")]
        public Task Iload1(IJavaThread thread) => Load<int>(thread, 1);

        [Opcode(0x1c, "iload_2")]
        public Task Iload2(IJavaThread thread) => Load<int>(thread, 2);

        [Opcode(0x1d, "iload_3")]
        public Task Iload3(IJavaThread thread) => Load<int>(thread, 3);

        [Opcode(0x1e, "lload_0")]
        public Task Lload0(IJavaThread thread) => Load<long>(thread, 0);

        [Opcode(0x1f, "lload_1")]
        public Task Lload1(IJavaThread thread) => Load<long>(thread, 1);

        [Opcode(0x20, "lload_2")]
        public Task Lload2(IJavaThread thread) => Load<long>(thread, 2);

        [Opcode(0x21, "lload_3")]
        public Task Lload3(IJavaThread thread) => Load<long>(thread, 3);

        [Opcode(0x22, "fload_0")]
        public Task Fload0(IJavaThread thread) => Load<float>(thread, 0);

        [Opcode(0x23, "fload_1")]
        public Task Fload1(IJavaThread thread) => Load<float>(thread, 1);

        [Opcode(0x24, "fload_2")]
        public Task Fload2(IJavaThread thread) => Load<float>(thread, 2);

        [Opcode(0x25, "fload_3")]
        public Task Fload3(IJavaThread thread) => Load<float>(thread, 3);

        [Opcode(0x26, "dload_0")]
        public Task Dload0(IJavaThread thread) => Load<double>(thread, 0);

        [Opcode(0x27, "dload_1")]
        public Task Dload1(IJavaThread thread) => Load<double>(thread, 1);

        [Opcode(0x28, "dload_2")]
        public Task Dload2(IJavaThread thread) => Load<double>(thread, 2);

        [Opcode(0x29, "dload_3")]
        public Task Dload3(IJavaThread thread) => Load<double>(thread, 3);

        [Opcode(0x2a, "aload_0")]
        public Task Aload0(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(thread.CurrentMethod.LocalVariables[0]);
            return Task.CompletedTask;
        }

        [Opcode(0x2b, "aload_1")]
        public Task Aload1(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(thread.CurrentMethod.LocalVariables[1]);
            return Task.CompletedTask;
        }

        [Opcode(0x2c, "aload_2")]
        public Task Aload2(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(thread.CurrentMethod.LocalVariables[2]);
            return Task.CompletedTask;
        }

        [Opcode(0x2d, "aload_3")]
        public Task Aload3(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(thread.CurrentMethod.LocalVariables[3]);
            return Task.CompletedTask;
        }

        [Opcode(0x2e, "iaload")]
        public Task Iaload(IJavaThread thread)
        {
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (!arrayref.HasValue)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_INT)
                throw new InvalidOperationException("The array type must be T_INT");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            // get the value
            var value = array.GetValue(index);

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x2f, "laload")]
        public Task Laload(IJavaThread thread)
        {
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (!arrayref.HasValue)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_LONG)
                throw new InvalidOperationException("The array type must be T_LONG");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            // get the value
            var value = array.GetValue(index);

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x30, "faload")]
        public Task Faload(IJavaThread thread)
        {
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (!arrayref.HasValue)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_FLOAT)
                throw new InvalidOperationException("The array type must be T_FLOAT");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            // get the value
            var value = array.GetValue(index);

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x31, "daload")]
        public Task Daload(IJavaThread thread)
        {
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (!arrayref.HasValue)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_DOUBLE)
                throw new InvalidOperationException("The array type must be T_DOUBLE");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            // get the value
            var value = array.GetValue(index);

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x32, "aaload")]
        public Task Aaload(IJavaThread thread)
        {
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapClassArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (!arrayref.HasValue)
                throw new NullReferenceException(nameof(arrayref));

            // get array from heap
            if (!_heap.TryGetClassArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            // get the value from array
            var value = (HeapClassRef)array.GetValue(index)!;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x33, "baload")]
        public Task Baload(IJavaThread thread)
        {
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (!arrayref.HasValue)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_BOOLEAN && arrayref?.PrimitiveType != ArrayTypes.T_BYTE)
                throw new InvalidOperationException("The array type must be T_BOOLEAN or T_BYTE");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            // get the value from array
            var value = (byte)array.GetValue(index)!;

            // sign-extend to int
            var result = (value & 0x80) == 0x80
                ? value - 0x100
                : value;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x34, "caload")]
        public Task Caload(IJavaThread thread)
        {
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (!arrayref.HasValue)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_CHAR)
                throw new InvalidOperationException("The array type must be T_CHAR");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            // get the value from array and zero-extend to int
            var value = Convert.ChangeType(array.GetValue(index), typeof(int));

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        [Opcode(0x35, "saload")]
        public Task Saload(IJavaThread thread)
        {
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (!arrayref.HasValue)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_SHORT)
                throw new InvalidOperationException("The array type must be T_SHORT");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            // get the value and sign-extend to int
            var value = Convert.ChangeType(array.GetValue(index), typeof(int));

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }

        private static Task Load<T>(IJavaThread thread, int index)
        {
            thread.CurrentMethod.OperandStack.Push((T?)thread.CurrentMethod.LocalVariables[index]);
            return Task.CompletedTask;
        }
    }
}
