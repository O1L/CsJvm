namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public class BootstrapMethod
    {
        public ushort BootstrapMethodRef { get; set; }
        public ushort NumBootstrapArguments { get; set; }
        public ushort[] BootstrapArguments { get; set; } = Array.Empty<ushort>();
    }

    public class BootstrapMethodsAttribute : AttributeInfo
    {
        public ushort NumBootstrapMethods { get; set; }
        public BootstrapMethod[] BootstrapMethods { get; set; } = Array.Empty<BootstrapMethod>();
    }
}
