namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class SourceDebugExtensionAttribute : AttributeInfo
    {
        public byte[] DebugExtension { get; set; } = Array.Empty<byte>();
    }
}
