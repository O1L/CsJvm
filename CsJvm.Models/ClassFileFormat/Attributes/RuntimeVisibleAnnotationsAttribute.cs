namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class RuntimeVisibleAnnotationsAttribute : AttributeInfo
    {
        public ushort NumAnnotations { get; set; }
        public Annotation[] Annotations { get; set; } = Array.Empty<Annotation>();
    }

    #region annotation
    public class Annotation
    {
        public ushort TypeIndex { get; set; }
        public ushort NumElementValuePairs { get; set; }
        public ElementValuePairs[] ElementValuePairs { get; set; } = Array.Empty<ElementValuePairs>();
    }
    #endregion

    public class ElementValue
    {
        public byte Tag { get; set; }
        public Value? Value { get; set; }

    }

    #region value
    public abstract class Value
    {
    }

    public class ConstValueIndex : Value
    {
        public ushort ValueIndex { get; set; }
    }

    public class EnumConstValue : Value
    {
        public ushort TypeNameIndex { get; set; }
        public ushort ConstNameIndex { get; set; }
    }

    public class ClassInfoIndex : Value
    {
        public ushort InfoIndex { get; set; }
    }

    public class AnnotationValue : Value
    {
        public Annotation? Value { get; set; }
    }

    public class ArrayValue : Value
    {
        public ushort NumValues { get; set; }
        public ElementValue[] Values { get; set; } = Array.Empty<ElementValue>();
    }
    #endregion

    #region element_value_pairs
    public class ElementValuePairs
    {
        public ushort ElementNameIndex { get; set; }
        public ElementValue? Value { get; set; }
    }
    #endregion
}
