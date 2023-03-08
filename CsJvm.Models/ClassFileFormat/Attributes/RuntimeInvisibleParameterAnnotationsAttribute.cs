namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class RuntimeInvisibleParameterAnnotationsAttribute : AttributeInfo
    {

        public byte NumParameters { get; set; }

        public ParameterAnnotation[] ParameterAnnotations { get; set; } = Array.Empty<ParameterAnnotation>();
    }
}
