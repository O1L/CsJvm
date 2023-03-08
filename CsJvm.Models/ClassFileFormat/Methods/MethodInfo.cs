using CsJvm.Models.ClassFileFormat.Attributes;

namespace CsJvm.Models.ClassFileFormat.Methods
{
    /// <summary>
    /// Java method info
    /// </summary>
    public class MethodInfo
    {
        /// <summary>
        /// A mask of flags used to denote access permission to and properties of this method.
        /// </summary>
        public MethodAccessAndPropertyFlags AccessFlags { get; set; }

        /// <summary>
        /// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure representing either 
        /// a valid unqualified name denoting a method, or (if this method is in a class rather than an interface) 
        /// the special method name &lt;init&gt;, or the special method name &lt;clinit&gt;.
        /// </summary>
        public ushort NameIndex { get; set; }

        /// <summary>
        /// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure representing a valid method descriptor.
        /// </summary>
        public ushort DescriptorIndex { get; set; }

        /// <summary>
        /// The value of the attributes_count item indicates the number of additional attributes of this method.
        /// </summary>
        public ushort AttributesCount { get; set; }

        /// <summary>
        /// Each value of the attributes table must be an <see cref="AttributeInfo"/> structure.
        /// </summary>
        public AttributeInfo[] Attributes { get; set; } = Array.Empty<AttributeInfo>();

        /// <summary>
        /// The method name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The method descriptor
        /// </summary>
        public string Descriptor { get; set; } = string.Empty;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $@"    ***Method info***
Method name: {Name}
Method descriptor: {Descriptor}
Access flags: 0x{AccessFlags:X4}
Name index: {NameIndex}
Descriptor index: {DescriptorIndex}
Attributes count: {AttributesCount}";
        }
    }
}
