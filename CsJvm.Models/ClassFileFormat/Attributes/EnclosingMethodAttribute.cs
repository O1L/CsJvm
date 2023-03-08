namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class EnclosingMethodAttribute : AttributeInfo
    {
        public ushort ClassIndex { get; set; }

        public ushort MethodIndex { get; set; }
    }
}
