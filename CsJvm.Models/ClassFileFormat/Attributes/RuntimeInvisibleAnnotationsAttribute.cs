namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class RuntimeInvisibleAnnotationsAttribute : AttributeInfo
    {
        public ushort NumAnnotations { get; set; }

        public Annotation[] Annotations { get; set; } = Array.Empty<Annotation>();
    }
}
