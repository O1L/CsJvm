using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models.ClassFileFormat;
using CsJvm.Models.Heap;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0x36, "istore")]
        public void Istore(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            Store<int>(thread, index);
        }

        [Opcode(0x37, "lstore")]
        public void Lstore(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            Store64<long>(thread, index);
        }

        [Opcode(0x38, "fstore")]
        public void Fstore(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            Store<float>(thread, index);
        }

        [Opcode(0x39, "dstore")]
        public void Dstore(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            Store64<double>(thread, index);
        }

        [Opcode(0x3a, "astore")]
        public void Astore(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            thread.CurrentMethod.LocalVariables[index] = thread.CurrentMethod.OperandStack.Pop();
        }

        [Opcode(0x3b, "istore_0")]
        public void Istore0(IJavaThread thread) => Store<int>(thread, 0);

        [Opcode(0x3c, "istore_1")]
        public void Istore1(IJavaThread thread) => Store<int>(thread, 1);

        [Opcode(0x3d, "istore_2")]
        public void Istore2(IJavaThread thread) => Store<int>(thread, 2);

        [Opcode(0x3e, "istore_3")]
        public void Istore3(IJavaThread thread) => Store<int>(thread, 3);

        [Opcode(0x3f, "lstore_0")]
        public void Lstore0(IJavaThread thread) => Store64<long>(thread, 0);

        [Opcode(0x40, "lstore_1")]
        public void Lstore1(IJavaThread thread) => Store64<long>(thread, 1);

        [Opcode(0x41, "lstore_2")]
        public void Lstore2(IJavaThread thread) => Store64<long>(thread, 2);

        [Opcode(0x42, "lstore_3")]
        public void Lstore3(IJavaThread thread) => Store64<long>(thread, 3);

        [Opcode(0x43, "fstore_0")]
        public void Fstore0(IJavaThread thread) => Store<float>(thread, 0);

        [Opcode(0x44, "fstore_1")]
        public void Fstore1(IJavaThread thread) => Store<float>(thread, 1);

        [Opcode(0x45, "fstore_2")]
        public void Fstore2(IJavaThread thread) => Store<float>(thread, 2);

        [Opcode(0x46, "fstore_3")]
        public void Fstore3(IJavaThread thread) => Store<float>(thread, 3);

        [Opcode(0x47, "dstore_0")]
        public void Dstore0(IJavaThread thread) => Store64<double>(thread, 0);

        [Opcode(0x48, "dstore_1")]
        public void Dstore1(IJavaThread thread) => Store64<double>(thread, 1);

        [Opcode(0x49, "dstore_2")]
        public void Dstore2(IJavaThread thread) => Store64<double>(thread, 2);

        [Opcode(0x4a, "dstore_3")]
        public void Dstore3(IJavaThread thread) => Store64<double>(thread, 3);

        [Opcode(0x4b, "astore_0")]
        public void Astore0(IJavaThread thread) => thread.CurrentMethod.LocalVariables[0] = thread.CurrentMethod.OperandStack.Pop();

        [Opcode(0x4c, "astore_1")]
        public void Astore1(IJavaThread thread) => thread.CurrentMethod.LocalVariables[1] = thread.CurrentMethod.OperandStack.Pop();

        [Opcode(0x4d, "astore_2")]
        public void Astore2(IJavaThread thread) => thread.CurrentMethod.LocalVariables[2] = thread.CurrentMethod.OperandStack.Pop();

        [Opcode(0x4e, "astore_3")]
        public void Astore3(IJavaThread thread) => thread.CurrentMethod.LocalVariables[3] = thread.CurrentMethod.OperandStack.Pop();

        [Opcode(0x4f, "iastore")]
        public void Iastore(IJavaThread thread)
        {
            var value = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (arrayref == null)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_INT)
                throw new InvalidOperationException("The array type must be T_INT");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            array.SetValue(value, index);
        }

        [Opcode(0x50, "lastore")]
        public void Lastore(IJavaThread thread)
        {
            var value = (long)thread.CurrentMethod.OperandStack.Pop()!;
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (arrayref == null)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_LONG)
                throw new InvalidOperationException("The array type must be T_LONG");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            array.SetValue(value, index);
        }

        [Opcode(0x51, "fastore")]
        public void Fastore(IJavaThread thread)
        {
            var value = (float)thread.CurrentMethod.OperandStack.Pop()!;
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (arrayref == null)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_FLOAT)
                throw new InvalidOperationException("The array type must be T_FLOAT");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            array.SetValue(value, index);
        }

        [Opcode(0x52, "dastore")]
        public void Dastore(IJavaThread thread)
        {
            var value = (double)thread.CurrentMethod.OperandStack.Pop()!;
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (arrayref == null)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_DOUBLE)
                throw new InvalidOperationException("The array type must be T_DOUBLE");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            array.SetValue(value, index);
        }

        [Opcode(0x53, "aastore")]
        public void Aastore(IJavaThread thread)
        {
            var value = (HeapClassRef)thread.CurrentMethod.OperandStack.Pop()!;
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapClassArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (arrayref == null)
                throw new NullReferenceException(nameof(arrayref));

            // get array from heap
            if (!_heap.TryGetClassArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            // set the value
            array.SetValue(value, index);
        }

        [Opcode(0x54, "bastore")]
        public void Bastore(IJavaThread thread)
        {
            var value = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (arrayref == null)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_BOOLEAN && arrayref?.PrimitiveType != ArrayTypes.T_BYTE)
                throw new InvalidOperationException("The array type must be T_BOOLEAN or T_BYTE");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            var valueToSet = arrayref?.PrimitiveType == ArrayTypes.T_BYTE
                ? (byte)value
                : (byte)(value & 0x1);

            // set the value
            array.SetValue(valueToSet, index);
        }

        [Opcode(0x55, "castore")]
        public void Castore(IJavaThread thread)
        {
            var value = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (arrayref == null)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_CHAR)
                throw new InvalidOperationException("The array type must be T_CHAR");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            array.SetValue((char)value, index);
        }

        [Opcode(0x56, "sastore")]
        public void Sastore(IJavaThread thread)
        {
            var value = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var index = (int)thread.CurrentMethod.OperandStack.Pop()!;
            var arrayref = (HeapArrayRef?)thread.CurrentMethod.OperandStack.Pop();

            if (arrayref == null)
                throw new NullReferenceException(nameof(arrayref));

            if (arrayref?.PrimitiveType != ArrayTypes.T_SHORT)
                throw new InvalidOperationException("The array type must be T_SHORT");

            // get array from heap
            if (!_heap.TryGetArray(arrayref.Value, out var array) || array == null)
                throw new NullReferenceException(nameof(array));

            // check bounds
            if (index >= array.Length)
                throw new IndexOutOfRangeException(nameof(index));

            array.SetValue((short)value, index);
        }

        /// <summary>
        /// Store value to local variables
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="thread">Current thread</param>
        /// <param name="index">Local variables index</param>
        private static void Store<T>(IJavaThread thread, int index)
            => thread.CurrentMethod.LocalVariables[index] = Convert.ChangeType(thread.CurrentMethod.OperandStack.Pop(), typeof(T));

        /// <summary>
        /// Store 64-bit value to local variables
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="thread">Current thread</param>
        /// <param name="index">Local variables index</param>
        private static void Store64<T>(IJavaThread thread, int index)
        {
            var value = Convert.ChangeType(thread.CurrentMethod.OperandStack.Pop(), typeof(T)); // (T?)thread.CurrentMethod.OperandStack.Pop();

            // the local variables at index and index+1 are set to value
            thread.CurrentMethod.LocalVariables[index + 0] = value;
            thread.CurrentMethod.LocalVariables[index + 1] = value;
        }

    }
}
