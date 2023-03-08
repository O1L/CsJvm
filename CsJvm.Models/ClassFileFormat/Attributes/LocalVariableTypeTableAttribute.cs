namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class LocalVariableTypeTableAttribute : AttributeInfo
    {
        public ushort LocalVariableTypeTableLength { get; set; }

        public LocalVariableTypeTable[] LocalVariableTypeTable { get; set; } = Array.Empty<LocalVariableTypeTable>();
    }
    public class LocalVariableTypeTable
    {
        public ushort StartPc { get; set; }
        public ushort Length { get; set; }
        public ushort NameIndex { get; set; }
        public ushort SignatureIndex { get; set; }
        public ushort Index { get; set; }
    }

}
