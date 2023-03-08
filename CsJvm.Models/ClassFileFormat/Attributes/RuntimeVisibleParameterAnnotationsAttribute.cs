namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class RuntimeVisibleParameterAnnotationsAttribute : AttributeInfo
    {
        public byte NumParameters { get; set; }
        public ParameterAnnotation[] ParameterAnnotations { get; set; } = Array.Empty<ParameterAnnotation>();
    }

    public class ParameterAnnotation
    {
        public ushort NumAnnotations { get; set; }
        public Annotation[] Annotations { get; set; } = Array.Empty<Annotation>();
    }
}
