using CsJvm.Abstractions.VirtualMachine;

namespace CsJvm.Abstractions.Instructions
{
    /// <summary>
    /// Java virtual machine opcodes set
    /// </summary>
    public interface IOpcodes
    {
        // Constant ops
        void Nop(IJavaThread thread);

        void AconstNull(IJavaThread thread);

        void IconstM1(IJavaThread thread);

        void Iconst0(IJavaThread thread);

        void Iconst1(IJavaThread thread);

        void Iconst2(IJavaThread thread);

        void Iconst3(IJavaThread thread);

        void Iconst4(IJavaThread thread);

        void Iconst5(IJavaThread thread);

        void Lconst0(IJavaThread thread);

        void Lconst1(IJavaThread thread);

        void Fconst0(IJavaThread thread);

        void Fconst1(IJavaThread thread);

        void Fconst2(IJavaThread thread);

        void Dconst0(IJavaThread thread);

        void Dconst1(IJavaThread thread);

        void Bipush(IJavaThread thread);

        void Sipush(IJavaThread thread);

        void Ldc(IJavaThread thread);

        void LdcW(IJavaThread thread);

        void Ldc2w(IJavaThread thread);

        // Load ops
        void Iload(IJavaThread thread);

        void Lload(IJavaThread thread);

        void Fload(IJavaThread thread);

        void Dload(IJavaThread thread);

        void Aload(IJavaThread thread);

        void Iload0(IJavaThread thread);

        void Iload1(IJavaThread thread);

        void Iload2(IJavaThread thread);

        void Iload3(IJavaThread thread);

        void Lload0(IJavaThread thread);

        void Lload1(IJavaThread thread);

        void Lload2(IJavaThread thread);

        void Lload3(IJavaThread thread);

        void Fload0(IJavaThread thread);

        void Fload1(IJavaThread thread);

        void Fload2(IJavaThread thread);

        void Fload3(IJavaThread thread);

        void Dload0(IJavaThread thread);

        void Dload1(IJavaThread thread);

        void Dload2(IJavaThread thread);

        void Dload3(IJavaThread thread);

        void Aload0(IJavaThread thread);

        void Aload1(IJavaThread thread);

        void Aload2(IJavaThread thread);

        void Aload3(IJavaThread thread);

        void Iaload(IJavaThread thread);

        void Laload(IJavaThread thread);

        void Faload(IJavaThread thread);

        void Daload(IJavaThread thread);

        void Aaload(IJavaThread thread);

        void Baload(IJavaThread thread);

        void Caload(IJavaThread thread);

        void Saload(IJavaThread thread);

        // Store ops
        void Istore(IJavaThread thread);

        void Lstore(IJavaThread thread);

        void Fstore(IJavaThread thread);

        void Dstore(IJavaThread thread);

        void Astore(IJavaThread thread);

        void Istore0(IJavaThread thread);

        void Istore1(IJavaThread thread);

        void Istore2(IJavaThread thread);

        void Istore3(IJavaThread thread);

        void Lstore0(IJavaThread thread);

        void Lstore1(IJavaThread thread);

        void Lstore2(IJavaThread thread);

        void Lstore3(IJavaThread thread);

        void Fstore0(IJavaThread thread);

        void Fstore1(IJavaThread thread);

        void Fstore2(IJavaThread thread);

        void Fstore3(IJavaThread thread);

        void Dstore0(IJavaThread thread);

        void Dstore1(IJavaThread thread);

        void Dstore2(IJavaThread thread);

        void Dstore3(IJavaThread thread);

        void Astore0(IJavaThread thread);

        void Astore1(IJavaThread thread);

        void Astore2(IJavaThread thread);

        void Astore3(IJavaThread thread);

        void Iastore(IJavaThread thread);

        void Lastore(IJavaThread thread);

        void Fastore(IJavaThread thread);

        void Dastore(IJavaThread thread);

        void Aastore(IJavaThread thread);

        void Bastore(IJavaThread thread);

        void Castore(IJavaThread thread);

        void Sastore(IJavaThread thread);

        // Stack ops
        void Pop(IJavaThread thread);

        void Pop2(IJavaThread thread);

        void Dup(IJavaThread thread);

        void DupX1(IJavaThread thread);

        void DupX2(IJavaThread thread);

        void Dup2(IJavaThread thread);

        void Dup2X1(IJavaThread thread);

        void Dup2X2(IJavaThread thread);

        void Swap(IJavaThread thread);

        // Math ops
        void Iadd(IJavaThread thread);

        void Ladd(IJavaThread thread);

        void Fadd(IJavaThread thread);

        void Dadd(IJavaThread thread);

        void Isub(IJavaThread thread);

        void Lsub(IJavaThread thread);

        void Fsub(IJavaThread thread);

        void Dsub(IJavaThread thread);

        void Imul(IJavaThread thread);

        void Lmul(IJavaThread thread);

        void Fmul(IJavaThread thread);

        void Dmul(IJavaThread thread);

        void Idiv(IJavaThread thread);

        void Ldiv(IJavaThread thread);

        void Fdiv(IJavaThread thread);

        void Ddiv(IJavaThread thread);

        void Irem(IJavaThread thread);

        void Lrem(IJavaThread thread);

        void Frem(IJavaThread thread);

        void Drem(IJavaThread thread);

        void Ineg(IJavaThread thread);

        void Lneg(IJavaThread thread);

        void Fneg(IJavaThread thread);

        void Dneg(IJavaThread thread);

        void Ishl(IJavaThread thread);

        void Lshl(IJavaThread thread);

        void Ishr(IJavaThread thread);

        void Lshr(IJavaThread thread);

        void Iushr(IJavaThread thread);

        void Lushr(IJavaThread thread);

        void Iand(IJavaThread thread);

        void Land(IJavaThread thread);

        void Ior(IJavaThread thread);

        void Lor(IJavaThread thread);

        void Ixor(IJavaThread thread);

        void Lxor(IJavaThread thread);

        void Iinc(IJavaThread thread);


        // Conversions ops
        void I2l(IJavaThread thread);

        void I2f(IJavaThread thread);

        void I2d(IJavaThread thread);

        void L2i(IJavaThread thread);

        void L2f(IJavaThread thread);

        void L2d(IJavaThread thread);

        void F2i(IJavaThread thread);

        void F2l(IJavaThread thread);

        void F2d(IJavaThread thread);

        void D2i(IJavaThread thread);

        void D2l(IJavaThread thread);

        void D2f(IJavaThread thread);

        void I2b(IJavaThread thread);

        void I2c(IJavaThread thread);

        void I2s(IJavaThread thread);


        // Comparisons ops
        void Lcmp(IJavaThread thread);

        void Fcmpl(IJavaThread thread);

        void Fcmpg(IJavaThread thread);

        void Dcmpl(IJavaThread thread);

        void Dcmpg(IJavaThread thread);

        void Ifeq(IJavaThread thread);

        void Ifne(IJavaThread thread);

        void Iflt(IJavaThread thread);

        void Ifge(IJavaThread thread);

        void Ifgt(IJavaThread thread);

        void Ifle(IJavaThread thread);

        void FfIcmpeq(IJavaThread thread);

        void FfIcmpne(IJavaThread thread);

        void FfIcmplt(IJavaThread thread);

        void FfIcmpge(IJavaThread thread);

        void FfIcmpgt(IJavaThread thread);

        void FfIcmple(IJavaThread thread);

        void IfAcmpeq(IJavaThread thread);

        void IfAcmpne(IJavaThread thread);


        // Control ops
        void Goto(IJavaThread thread);

        void Jsr(IJavaThread thread);

        void Ret(IJavaThread thread);

        void TableSwitch(IJavaThread thread);

        void LookupSwitch(IJavaThread thread);

        void Ireturn(IJavaThread thread);

        void Lreturn(IJavaThread thread);

        void Freturn(IJavaThread thread);

        void Dreturn(IJavaThread thread);

        void Areturn(IJavaThread thread);

        void Return(IJavaThread thread);


        // References ops
        void GetStatic(IJavaThread thread);

        void PutStatic(IJavaThread thread);

        void GetField(IJavaThread thread);

        void PutField(IJavaThread thread);

        void InvokeVirtual(IJavaThread thread);

        void InvokeSpecial(IJavaThread thread);

        void InvokeStatic(IJavaThread thread);

        void InvokeInterface(IJavaThread thread);

        void InvokeDynamic(IJavaThread thread);

        void New(IJavaThread thread);

        void NewArray(IJavaThread thread);

        void AnewArray(IJavaThread thread);

        void ArrayLength(IJavaThread thread);

        void Athrow(IJavaThread thread);

        void CheckCast(IJavaThread thread);

        void InstanceOf(IJavaThread thread);

        void MonitorEnter(IJavaThread thread);

        void MonitorExit(IJavaThread thread);


        // Extended ops
        void Wide(IJavaThread thread);

        void MultiAnewArray(IJavaThread thread);

        void IfNull(IJavaThread thread);

        void IfNonNull(IJavaThread thread);

        void GotoW(IJavaThread thread);

        void JsrW(IJavaThread thread);


        // Reserved ops
        void Breakpoint(IJavaThread thread);

        void Impdep1(IJavaThread thread);

        void Impdep2(IJavaThread thread);
    }
}
