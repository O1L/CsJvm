using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models.ClassFileFormat.ConstantPool;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0x00, "nop")]
        public Task Nop(IJavaThread thread)
        {
            return Task.CompletedTask;
        }

        [Opcode(0x01, "aconst_null")]
        public Task AconstNull(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(null);
            return Task.CompletedTask;
        }

        [Opcode(0x02, "iconst_m1")]
        public Task IconstM1(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(-1);
            return Task.CompletedTask;
        }

        [Opcode(0x03, "iconst_0")]
        public Task Iconst0(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(0);
            return Task.CompletedTask;
        }

        [Opcode(0x04, "iconst_1")]
        public Task Iconst1(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(1);
            return Task.CompletedTask;
        }

        [Opcode(0x05, "iconst_2")]
        public Task Iconst2(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(2);
            return Task.CompletedTask;
        }

        [Opcode(0x06, "iconst_3")]
        public Task Iconst3(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(3);
            return Task.CompletedTask;
        }

        [Opcode(0x07, "iconst_4")]
        public Task Iconst4(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(4);
            return Task.CompletedTask;
        }

        [Opcode(0x08, "iconst_5")]
        public Task Iconst5(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(5);
            return Task.CompletedTask;
        }

        [Opcode(0x09, "lconst_0")]
        public Task Lconst0(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(0L);
            return Task.CompletedTask;
        }

        [Opcode(0x0a, "lconst_1")]
        public Task Lconst1(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(1L);
            return Task.CompletedTask;
        }

        [Opcode(0x0b, "fconst_0")]
        public Task Fconst0(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(0F);
            return Task.CompletedTask;
        }

        [Opcode(0x0c, "fconst_1")]
        public Task Fconst1(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(1F);
            return Task.CompletedTask;
        }

        [Opcode(0x0d, "fconst_2")]
        public Task Fconst2(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(2F);
            return Task.CompletedTask;
        }

        [Opcode(0x0e, "dconst_0")]
        public Task Dconst0(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(0D);
            return Task.CompletedTask;
        }

        [Opcode(0x0f, "dconst_1")]
        public Task Dconst1(IJavaThread thread)
        {
            thread.CurrentMethod.OperandStack.Push(1D);
            return Task.CompletedTask;
        }

        [Opcode(0x10, "bipush")]
        public Task Bipush(IJavaThread thread)
        {
            var value = thread.CurrentMethod.Code[thread.ProgramCounter++];

            // sign-extend to int
            var result = (value & 0x80) == 0x80
                ? value - 0x100
                : value;

            thread.CurrentMethod.OperandStack.Push(result);
            return Task.CompletedTask;
        }

        [Opcode(0x11, "sipush")]
        public Task Sipush(IJavaThread thread)
        {
            var byte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var byte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var value = (short)(byte1 << 8) | (short)byte2;

            thread.CurrentMethod.OperandStack.Push(value);
            return Task.CompletedTask;
        }


        [Opcode(0x12, "ldc")]
        public async Task Ldc(IJavaThread thread)
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
                        var stringClass = await _runtime.GetClassAsync("java/lang/String") ?? throw new InvalidOperationException("Cannot resolve string class");

                        // set string value
                        stringClass.Fields["value:[C"] = value.ToCharArray();

                        // create heap ref
                        var classRef = _heap.CreateClassRef(stringClass);

                        // push ref to stack
                        thread.CurrentMethod.OperandStack.Push(classRef);

                        // initialize class
                        await RunDefaultCtorAsync(stringClass, thread, classRef);
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
        public Task LdcW(IJavaThread thread)
        {
            var indexbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var indexbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var index = (indexbyte1 << 8) | indexbyte2;
            return Ldc(thread, index);
        }

        [Opcode(0x14, "ldc2_w")]
        public Task Ldc2w(IJavaThread thread)
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
            return Task.CompletedTask;
        }

        private static Task Ldc(IJavaThread thread, int index)
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

            return Task.CompletedTask;
        }
    }
}
