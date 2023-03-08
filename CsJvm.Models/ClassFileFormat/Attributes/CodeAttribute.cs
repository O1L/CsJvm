namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class CodeAttribute : AttributeInfo
    {
        public ushort MaxStack { get; set; }
        public ushort MaxLocals { get; set; }
        public uint CodeLength { get; set; }
        public byte[] Code { get; set; } = Array.Empty<byte>();
        public ushort ExceptionTableLength { get; set; }
        public ExceptionTable[] ExceptionTable { get; set; } = Array.Empty<ExceptionTable>();
        public ushort AttributesCount { get; set; }
        public AttributeInfo[] Attributes { get; set; } = Array.Empty<AttributeInfo>();
    }

    public class ExceptionTable
    {
        public ushort StartPc { get; set; }

        public ushort EndPc { get; set; }

        public ushort HandlerPc { get; set; }

        public ushort CatchType { get; set; }
    }
}
