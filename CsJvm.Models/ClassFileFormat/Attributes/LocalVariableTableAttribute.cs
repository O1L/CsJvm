namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class LocalVariableTableAttribute : AttributeInfo
    {
        public ushort LocalVariableTableLength { get; set; }

        public LocalVariableTable[] LocalVariableTable { get; set; } = Array.Empty<LocalVariableTable>();
    }

    public class LocalVariableTable
    {
        public ushort StartPc { get; set; }
        public ushort Length { get; set; }
        public ushort NameIndex { get; set; }
        public ushort DescriptorIndex { get; set; }
        public ushort Index { get; set; }
    }

}
