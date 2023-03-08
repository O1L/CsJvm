namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class ExceptionsAttribute : AttributeInfo
    {
        public ushort NumberOfExceptions { get; set; }

        public ushort[] ExceptionIndexTable { get; set; } = Array.Empty<ushort>();
    }
}
