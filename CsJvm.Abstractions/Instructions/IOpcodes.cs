using CsJvm.Abstractions.VirtualMachine;

namespace CsJvm.Abstractions.Instructions
{
    /// <summary>
    /// Java virtual machine opcodes set
    /// </summary>
    public interface IOpcodes
    {
        // Constant ops
        Task Nop(IJavaThread thread);

        Task AconstNull(IJavaThread thread);

        Task IconstM1(IJavaThread thread);

        Task Iconst0(IJavaThread thread);

        Task Iconst1(IJavaThread thread);

        Task Iconst2(IJavaThread thread);

        Task Iconst3(IJavaThread thread);

        Task Iconst4(IJavaThread thread);

        Task Iconst5(IJavaThread thread);

        Task Lconst0(IJavaThread thread);

        Task Lconst1(IJavaThread thread);

        Task Fconst0(IJavaThread thread);

        Task Fconst1(IJavaThread thread);

        Task Fconst2(IJavaThread thread);

        Task Dconst0(IJavaThread thread);

        Task Dconst1(IJavaThread thread);

        Task Bipush(IJavaThread thread);

        Task Sipush(IJavaThread thread);

        Task Ldc(IJavaThread thread);

        Task LdcW(IJavaThread thread);

        Task Ldc2w(IJavaThread thread);

        // Load ops
        Task Iload(IJavaThread thread);

        Task Lload(IJavaThread thread);

        Task Fload(IJavaThread thread);

        Task Dload(IJavaThread thread);

        Task Aload(IJavaThread thread);

        Task Iload0(IJavaThread thread);

        Task Iload1(IJavaThread thread);

        Task Iload2(IJavaThread thread);

        Task Iload3(IJavaThread thread);

        Task Lload0(IJavaThread thread);

        Task Lload1(IJavaThread thread);

        Task Lload2(IJavaThread thread);

        Task Lload3(IJavaThread thread);

        Task Fload0(IJavaThread thread);

        Task Fload1(IJavaThread thread);

        Task Fload2(IJavaThread thread);

        Task Fload3(IJavaThread thread);

        Task Dload0(IJavaThread thread);

        Task Dload1(IJavaThread thread);

        Task Dload2(IJavaThread thread);

        Task Dload3(IJavaThread thread);

        Task Aload0(IJavaThread thread);

        Task Aload1(IJavaThread thread);

        Task Aload2(IJavaThread thread);

        Task Aload3(IJavaThread thread);

        Task Iaload(IJavaThread thread);

        Task Laload(IJavaThread thread);

        Task Faload(IJavaThread thread);

        Task Daload(IJavaThread thread);

        Task Aaload(IJavaThread thread);

        Task Baload(IJavaThread thread);

        Task Caload(IJavaThread thread);

        Task Saload(IJavaThread thread);

        // Store ops
        Task Istore(IJavaThread thread);

        Task Lstore(IJavaThread thread);

        Task Fstore(IJavaThread thread);

        Task Dstore(IJavaThread thread);

        Task Astore(IJavaThread thread);

        Task Istore0(IJavaThread thread);

        Task Istore1(IJavaThread thread);

        Task Istore2(IJavaThread thread);

        Task Istore3(IJavaThread thread);

        Task Lstore0(IJavaThread thread);

        Task Lstore1(IJavaThread thread);

        Task Lstore2(IJavaThread thread);

        Task Lstore3(IJavaThread thread);

        Task Fstore0(IJavaThread thread);

        Task Fstore1(IJavaThread thread);

        Task Fstore2(IJavaThread thread);

        Task Fstore3(IJavaThread thread);

        Task Dstore0(IJavaThread thread);

        Task Dstore1(IJavaThread thread);

        Task Dstore2(IJavaThread thread);

        Task Dstore3(IJavaThread thread);

        Task Astore0(IJavaThread thread);

        Task Astore1(IJavaThread thread);

        Task Astore2(IJavaThread thread);

        Task Astore3(IJavaThread thread);

        Task Iastore(IJavaThread thread);

        Task Lastore(IJavaThread thread);

        Task Fastore(IJavaThread thread);

        Task Dastore(IJavaThread thread);

        Task Aastore(IJavaThread thread);

        Task Bastore(IJavaThread thread);

        Task Castore(IJavaThread thread);

        Task Sastore(IJavaThread thread);

        // Stack ops
        Task Pop(IJavaThread thread);

        Task Pop2(IJavaThread thread);

        Task Dup(IJavaThread thread);

        Task DupX1(IJavaThread thread);

        Task DupX2(IJavaThread thread);

        Task Dup2(IJavaThread thread);

        Task Dup2X1(IJavaThread thread);

        Task Dup2X2(IJavaThread thread);

        Task Swap(IJavaThread thread);

        // Math ops
        Task Iadd(IJavaThread thread);

        Task Ladd(IJavaThread thread);

        Task Fadd(IJavaThread thread);

        Task Dadd(IJavaThread thread);

        Task Isub(IJavaThread thread);

        Task Lsub(IJavaThread thread);

        Task Fsub(IJavaThread thread);

        Task Dsub(IJavaThread thread);

        Task Imul(IJavaThread thread);

        Task Lmul(IJavaThread thread);

        Task Fmul(IJavaThread thread);

        Task Dmul(IJavaThread thread);

        Task Idiv(IJavaThread thread);

        Task Ldiv(IJavaThread thread);

        Task Fdiv(IJavaThread thread);

        Task Ddiv(IJavaThread thread);

        Task Irem(IJavaThread thread);

        Task Lrem(IJavaThread thread);

        Task Frem(IJavaThread thread);

        Task Drem(IJavaThread thread);

        Task Ineg(IJavaThread thread);

        Task Lneg(IJavaThread thread);

        Task Fneg(IJavaThread thread);

        Task Dneg(IJavaThread thread);

        Task Ishl(IJavaThread thread);

        Task Lshl(IJavaThread thread);

        Task Ishr(IJavaThread thread);

        Task Lshr(IJavaThread thread);

        Task Iushr(IJavaThread thread);

        Task Lushr(IJavaThread thread);

        Task Iand(IJavaThread thread);

        Task Land(IJavaThread thread);

        Task Ior(IJavaThread thread);

        Task Lor(IJavaThread thread);

        Task Ixor(IJavaThread thread);

        Task Lxor(IJavaThread thread);

        Task Iinc(IJavaThread thread);


        // Conversions ops
        Task I2l(IJavaThread thread);

        Task I2f(IJavaThread thread);

        Task I2d(IJavaThread thread);

        Task L2i(IJavaThread thread);

        Task L2f(IJavaThread thread);

        Task L2d(IJavaThread thread);

        Task F2i(IJavaThread thread);

        Task F2l(IJavaThread thread);

        Task F2d(IJavaThread thread);

        Task D2i(IJavaThread thread);

        Task D2l(IJavaThread thread);

        Task D2f(IJavaThread thread);

        Task I2b(IJavaThread thread);

        Task I2c(IJavaThread thread);

        Task I2s(IJavaThread thread);


        // Comparisons ops
        Task Lcmp(IJavaThread thread);

        Task Fcmpl(IJavaThread thread);

        Task Fcmpg(IJavaThread thread);

        Task Dcmpl(IJavaThread thread);

        Task Dcmpg(IJavaThread thread);

        Task Ifeq(IJavaThread thread);

        Task Ifne(IJavaThread thread);

        Task Iflt(IJavaThread thread);

        Task Ifge(IJavaThread thread);

        Task Ifgt(IJavaThread thread);

        Task Ifle(IJavaThread thread);

        Task FfIcmpeq(IJavaThread thread);

        Task FfIcmpne(IJavaThread thread);

        Task FfIcmplt(IJavaThread thread);

        Task FfIcmpge(IJavaThread thread);

        Task FfIcmpgt(IJavaThread thread);

        Task FfIcmple(IJavaThread thread);

        Task IfAcmpeq(IJavaThread thread);

        Task IfAcmpne(IJavaThread thread);


        // Control ops
        Task Goto(IJavaThread thread);

        Task Jsr(IJavaThread thread);

        Task Ret(IJavaThread thread);

        Task TableSwitch(IJavaThread thread);

        Task LookupSwitch(IJavaThread thread);

        Task Ireturn(IJavaThread thread);

        Task Lreturn(IJavaThread thread);

        Task Freturn(IJavaThread thread);

        Task Dreturn(IJavaThread thread);

        Task Areturn(IJavaThread thread);

        Task Return(IJavaThread thread);


        // References ops
        Task GetStatic(IJavaThread thread);

        Task PutStatic(IJavaThread thread);

        Task GetField(IJavaThread thread);

        Task PutField(IJavaThread thread);

        Task InvokeVirtual(IJavaThread thread);

        Task InvokeSpecial(IJavaThread thread);

        Task InvokeStatic(IJavaThread thread);

        Task InvokeInterface(IJavaThread thread);

        Task InvokeDynamic(IJavaThread thread);

        Task New(IJavaThread thread);

        Task NewArray(IJavaThread thread);

        Task AnewArray(IJavaThread thread);

        Task ArrayLength(IJavaThread thread);

        Task Athrow(IJavaThread thread);

        Task CheckCast(IJavaThread thread);

        Task InstanceOf(IJavaThread thread);

        Task MonitorEnter(IJavaThread thread);

        Task MonitorExit(IJavaThread thread);


        // Extended ops
        Task Wide(IJavaThread thread);

        Task MultiAnewArray(IJavaThread thread);

        Task IfNull(IJavaThread thread);

        Task IfNonNull(IJavaThread thread);

        Task GotoW(IJavaThread thread);

        Task JsrW(IJavaThread thread);


        // Reserved ops
        Task Breakpoint(IJavaThread thread);

        Task Impdep1(IJavaThread thread);

        Task Impdep2(IJavaThread thread);
    }
}
