using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models.ClassFileFormat.ConstantPool;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0x00, "nop")]
        public void Nop(IJavaThread thread)
        {
        }

        [Opcode(0x01, "aconst_null")]
        public void AconstNull(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(null);

        [Opcode(0x02, "iconst_m1")]
        public void IconstM1(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(-1);

        [Opcode(0x03, "iconst_0")]
        public void Iconst0(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(0);

        [Opcode(0x04, "iconst_1")]
        public void Iconst1(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(1);

        [Opcode(0x05, "iconst_2")]
        public void Iconst2(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(2);

        [Opcode(0x06, "iconst_3")]
        public void Iconst3(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(3);

        [Opcode(0x07, "iconst_4")]
        public void Iconst4(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(4);

        [Opcode(0x08, "iconst_5")]
        public void Iconst5(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(5);

        [Opcode(0x09, "lconst_0")]
        public void Lconst0(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(0L);

        [Opcode(0x0a, "lconst_1")]
        public void Lconst1(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(1L);

        [Opcode(0x0b, "fconst_0")]
        public void Fconst0(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(0F);

        [Opcode(0x0c, "fconst_1")]
        public void Fconst1(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(1F);

        [Opcode(0x0d, "fconst_2")]
        public void Fconst2(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(2F);

        [Opcode(0x0e, "dconst_0")]
        public void Dconst0(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(0D);

        [Opcode(0x0f, "dconst_1")]
        public void Dconst1(IJavaThread thread) => thread.CurrentMethod.OperandStack.Push(1D);

        [Opcode(0x10, "bipush")]
        public void Bipush(IJavaThread thread)
        {
            var value = thread.CurrentMethod.Code[thread.ProgramCounter++];

            // sign-extend to int
            var result = (value & 0x80) == 0x80
                ? value - 0x100
                : value;

            thread.CurrentMethod.OperandStack.Push(result);
        }

        [Opcode(0x11, "sipush")]
        public void Sipush(IJavaThread thread)
        {
            var byte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var byte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var value = (short)(byte1 << 8) | (short)byte2;

            thread.CurrentMethod.OperandStack.Push(value);
        }


        [Opcode(0x12, "ldc")]
        public void Ldc(IJavaThread thread)
        {
            var index = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var info = thread.CurrentMethod.CpInfo[index];

            switch (info)
            {
                case CONSTANT_ClassInfo classInfo:
                    {
                        var name = ((CONSTANT_Utf8Info)thread.CurrentMethod.CpInfo[classInfo.NameIndex]).Utf8String;
                        throw new NotImplementedException(name);

                        /*var value = _runtime.GetJavaClass(name);
                        RunInit(frame, decoder, value);

                        thread.CurrentMethod.OperandStack.Push(value);*/
                    }

                case CONSTANT_StringInfo stringInfo:
                    {
                        var value = ((CONSTANT_Utf8Info)thread.CurrentMethod.CpInfo[stringInfo.StringIndex]).Utf8String;

                        if (!_runtime.TryGet("java/lang/String", out var stringClass) || stringClass == null)
                            throw new InvalidOperationException("Cannot resolve string class");

                        // set string value
                        stringClass.Fields["value:[C"] = value.ToCharArray();

                        // create heap ref
                        var classRef = _heap.CreateClassRef(stringClass);

                        // push ref to stack
                        thread.CurrentMethod.OperandStack.Push(classRef);

                        // initialize class
                        RunDefaultCtor(stringClass, thread, classRef);
                    }
                    break;

                case CONSTANT_IntegerInfo integerInfo:
                    thread.CurrentMethod.OperandStack.Push(integerInfo.Value);
                    break;

                case CONSTANT_FloatInfo floatInfo:
                    thread.CurrentMethod.OperandStack.Push(floatInfo.Value);
                    break;

                default:
                    throw new InvalidOperationException($"Bad info type: {info.GetType()}");
            }
        }

        [Opcode(0x13, "ldc_w")]
        public void LdcW(IJavaThread thread)
        {
            var indexbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var indexbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var index = (indexbyte1 << 8) | indexbyte2;
            Ldc(thread, index);
        }

        [Opcode(0x14, "ldc2_w")]
        public void Ldc2w(IJavaThread thread)
        {
            var indexbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var indexbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var index = (indexbyte1 << 8) | indexbyte2;

            var info = thread.CurrentMethod.CpInfo[index];

            switch (info)
            {
                case CONSTANT_LongInfo longInfo:
                    thread.CurrentMethod.OperandStack.Push(longInfo.Value);
                    break;

                case CONSTANT_DoubleInfo doubleInfo:
                    thread.CurrentMethod.OperandStack.Push(doubleInfo.Value);
                    break;

                default:
                    throw new NotImplementedException($"A symbolic reference to {info.GetType()}");
            }
        }

        private static void Ldc(IJavaThread thread, int index)
        {
            var info = thread.CurrentMethod.CpInfo[index];

            switch (info)
            {
                case CONSTANT_ClassInfo classInfo:
                    {
                        var name = ((CONSTANT_Utf8Info)thread.CurrentMethod.CpInfo[classInfo.NameIndex]).Utf8String;

                        throw new NotImplementedException(name);

                        //var value = _runtime.GetJavaClass(name);
                        //RunInit(frame, decoder, value);
                        //thread.CurrentMethod.OperandStack.Push(value);
                    }

                case CONSTANT_StringInfo stringInfo:
                    {
                        var value = ((CONSTANT_Utf8Info)thread.CurrentMethod.CpInfo[stringInfo.StringIndex]).Utf8String;
                        throw new NotImplementedException(value);

                        //var stringClass = _runtime.GetJavaClass("java/lang/String");
                        //RunInit(frame, decoder, stringClass);
                        //stringClass.Fields["value:[C"] = value.ToCharArray();
                        //thread.CurrentMethod.OperandStack.Push(stringClass);
                    }

                case CONSTANT_IntegerInfo integerInfo:
                    thread.CurrentMethod.OperandStack.Push(integerInfo.Value);
                    break;

                case CONSTANT_FloatInfo floatInfo:
                    thread.CurrentMethod.OperandStack.Push(floatInfo.Value);
                    break;

                default:
                    throw new ArgumentException("Bad info");
            }
        }
    }
}
