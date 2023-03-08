namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class InnerClassesAttribute : AttributeInfo
    {

        public ushort NumberOfClasses { get; set; }

        public Classes[] Classes { get; set; } = Array.Empty<Classes>();
    }

    public class Classes
    {
        public ushort InnerClassInfoIndex { get; set; }
        public ushort OuterClassInfoIndex { get; set; }
        public ushort InnerNameIndex { get; set; }
        public ushort InnerClassAccessFlags { get; set; }
    }
}
