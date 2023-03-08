namespace CsJvm.Models.ClassFileFormat.ConstantPool
{
    public enum BytecodeBehaviorsForMethodHandles
    {
        REF_getField = 1,
        REF_getStatic,
        REF_putField,
        REF_putStatic,
        REF_invokeVirtual,
        REF_invokeStatic,
        REF_invokeSpecial,
        REF_newInvokeSpecial,
        REF_invokeInterface
    }
}
