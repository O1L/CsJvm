namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class NestMembersAttribute : AttributeInfo
    {
        public ushort NumberOfClasses { get; set; }

        public ushort[] Classes { get; set; } = Array.Empty<ushort>();
    }
}
