using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models.ClassFileFormat;
using CsJvm.Models.Heap;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0x15, "iload")]
        public void Iload(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            Load<int>(thread, index);
        }

        [Opcode(0x16, "lload")]
        public void Lload(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            Load<long>(thread, index);
        }

        [Opcode(0x17, "fload")]
        public void Fload(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            Load<float>(thread, index);
        }

        [Opcode(0x18, "dload")]
        public void Dload(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            Load<double>(thread, index);
        }

        [Opcode(0x19, "aload")]
        public void Aload(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            thread.CurrentMethod.OperandStack.Push(thread.CurrentMethod.LocalVariables[index]);
        }

        [Opcode(0x1a, "iload_0")]
        public void Iload0(IJavaThread thread) => Load<int>(thread, 0);

        [Opcode(0x1b, "iload_1")]
        public void Iload1(IJavaThread thread) => Load<int>(thread, 1);

        [Opcode(0x1c, "iload_2")]
        public void Iload2(IJavaThread thread) => Load<int>(thread, 2);

        [Opcode(0x1d, "iload_3")]
        public void Iload3(IJavaThread thread) => Load<int>(thread, 3);

        [Opcode(0x1e, "lload_0")]
        public void Lload0(IJavaThread thread) => Load<long>(thread, 0);

        [Opcode(0x1f, "lload_1")]
        public void Lload1(IJavaThread thread) => Load<long>(thread, 1);

        [Opcode(0x20, "lload_2")]
        public void Lload2(IJavaThread thread) => Load<long>(thread, 2);

        [Opcode(0x21, "lload_3")]
        public void Lload3(IJavaThread thread) => Load<long>(thread, 3);

        [Opcode(0x22, "fload_0")]
        public void Fload0(IJavaThread thread) => Load<float>(thread, 0);

        [Opcode(0x23, "fload_1")]
        public void Fload1(IJavaThread thread) => Load<float>(thread, 1);

        [Opcode(0x24, "fload_2")]
        public void Fload2(IJavaThread thread) => Load<float>(thread, 2);

        [Opcode(0x25, "fload_3")]
        public void Fload3(IJavaThread thread) => Load<float>(thread, 3);

        [Opcode(0x26, "dload_0")]
        public void Dload0(IJavaThread thread) => Load<double>(thread, 0);

        [Opcode(0x27, "dload_1")]
        public void Dload1(IJavaThread thread) => Load<double>(thread, 1);

        [Opcode(0x28, "dload_2")]
        public void Dload2(IJavaThread thread) => Load<double>(thread, 2);

        [Opcode(0x29, "dload_3")]
        public void Dload3(IJavaThread thread) => Load<double>(thread, 3);

        [Opcode(0x2a, "aload_0")]
        public void Aload0(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(thread.CurrentMethod.LocalVariables[0]);

        [Opcode(0x2b, "aload_1")]
        public void Aload1(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(thread.CurrentMethod.LocalVariables[1]);

        [Opcode(0x2c, "aload_2")]
        public void Aload2(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(thread.CurrentMethod.LocalVariables[2]);

        [Opcode(0x2d, "aload_3")]
        public void Aload3(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(thread.CurrentMethod.LocalVariables[3]);

        [Opcode(0x2e, "iaload")]
        public void Iaload(IJavaThread thread)
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

        }

        [Opcode(0x2f, "laload")]
        public void Laload(IJavaThread thread)
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
        }

        [Opcode(0x30, "faload")]
        public void Faload(IJavaThread thread)
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
        }

        [Opcode(0x31, "daload")]
        public void Daload(IJavaThread thread)
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
        }

        [Opcode(0x32, "aaload")]
        public void Aaload(IJavaThread thread)
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
        }

        [Opcode(0x33, "baload")]
        public void Baload(IJavaThread thread)
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
        }

        [Opcode(0x34, "caload")]
        public void Caload(IJavaThread thread)
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
        }

        [Opcode(0x35, "saload")]
        public void Saload(IJavaThread thread)
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
        }

        private static void Load<T>(IJavaThread thread, int index)
            => thread.CurrentMethod.OperandStack.Push((T?)thread.CurrentMethod.LocalVariables[index]);
    }
}
