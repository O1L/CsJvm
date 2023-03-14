using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0xca, "breakpoint")]
        public Task Breakpoint(IJavaThread thread) => throw new NotImplementedException("breakpoint");

        [Opcode(0xfe, "impdep1")]
        public Task Impdep1(IJavaThread thread) => throw new NotImplementedException("impdep1");

        [Opcode(0xff, "impdep2")]
        public Task Impdep2(IJavaThread thread) => throw new NotImplementedException("impdep2");
    }
}
