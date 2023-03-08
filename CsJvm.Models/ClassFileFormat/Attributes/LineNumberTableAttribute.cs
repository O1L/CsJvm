namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class LineNumberTableAttribute : AttributeInfo
    {

        public ushort LineNumberTableLength { get; set; }

        public LineNumberTable[] LineNumberTable { get; set; } = Array.Empty<LineNumberTable>();
    }

    public class LineNumberTable
    {
        public ushort StartPc { get; set; }
        public ushort LineNumber { get; set; }
    }
}
