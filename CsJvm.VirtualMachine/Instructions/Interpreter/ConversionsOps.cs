using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0x85, "i2l")]
        public void I2l(IJavaThread thread) => ConvertToType<int, long>(thread);

        [Opcode(0x86, "i2f")]
        public void I2f(IJavaThread thread) => ConvertToType<int, float>(thread);

        [Opcode(0x87, "i2d")]
        public void I2d(IJavaThread thread) => ConvertToType<int, double>(thread);

        [Opcode(0x88, "l2i")]
        public void L2i(IJavaThread thread) => ConvertToType<long, int>(thread);

        [Opcode(0x89, "l2f")]
        public void L2f(IJavaThread thread) => ConvertToType<long, float>(thread);

        [Opcode(0x8a, "l2d")]
        public void L2d(IJavaThread thread) => ConvertToType<long, double>(thread);

        [Opcode(0x8b, "f2i")]
        public void F2i(IJavaThread thread) => ConvertToType<float, int>(thread);

        [Opcode(0x8c, "f2l")]
        public void F2l(IJavaThread thread) => ConvertToType<float, long>(thread);

        [Opcode(0x8d, "f2d")]
        public void F2d(IJavaThread thread) => ConvertToType<float, double>(thread);

        [Opcode(0x8e, "d2i")]
        public void D2i(IJavaThread thread) => ConvertToType<double, int>(thread);

        [Opcode(0x8f, "d2l")]
        public void D2l(IJavaThread thread) => ConvertToType<double, long>(thread);

        [Opcode(0x90, "d2f")]
        public void D2f(IJavaThread thread) => ConvertToType<double, float>(thread);

        [Opcode(0x91, "i2b")]
        public void I2b(IJavaThread thread) => ConvertToType<int, byte>(thread);

        [Opcode(0x92, "i2c")]
        public void I2c(IJavaThread thread) => ConvertToType<int, char>(thread);

        [Opcode(0x93, "i2s")]
        public void I2s(IJavaThread thread) => ConvertToType<int, short>(thread);

        /// <summary>
        /// Converts value
        /// </summary>
        /// <typeparam name="From">Base type</typeparam>
        /// <typeparam name="To">Target type</typeparam>
        /// <param name="thread">Current thread</param>
        private static void ConvertToType<From, To>(IJavaThread thread)
        {
            var value = (From?)thread.CurrentMethod.OperandStack.Pop();

            object? result = value switch
            {
                float floatValue => Convert.ChangeType(MathF.Truncate(floatValue), typeof(To)),
                double doubleValue => Convert.ChangeType(Math.Truncate(doubleValue), typeof(To)),
                _ => Convert.ChangeType(value, typeof(To))
            };

            thread.CurrentMethod.OperandStack.Push(result);
        }
    }
}
