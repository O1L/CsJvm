using CsJvm.Models.ClassFileFormat.Attributes;

namespace CsJvm.Models.ClassFileFormat.Fields
{
    /// <summary>
    /// Field information
    /// </summary>
    public class FieldInfo
    {
        /// <summary>
        /// A mask of flags used to denote access permission to and properties of this field.
        /// </summary>
        public FieldAccessAndPropertyFlags AccessFlags { get; set; }

        /// <summary>
        /// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure which represents a valid unqualified name denoting a field.
        /// </summary>
        public ushort NameIndex { get; set; }

        /// <summary>
        /// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure which represents a valid field descriptor.
        /// </summary>
        public ushort DescriptorIndex { get; set; }

        /// <summary>
        /// The value of the attributes_count item indicates the number of additional attributes of this field.
        /// </summary>
        public ushort AttributesCount { get; set; }

        /// <summary>
        /// Each value of the attributes table must be an <see cref="AttributeInfo"/> structure.
        /// </summary>
        public AttributeInfo[] Attributes { get; set; } = Array.Empty<AttributeInfo>();

        /// <summary>
        /// The field name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The field descriptor
        /// </summary>
        public string Descriptor { get; set; } = string.Empty;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $@"    ***Field info***
Field name: {Name}
Descriptor: {Descriptor}
Access flags: 0x{AccessFlags:X4}
Name index: {NameIndex}
Descriptor index: {DescriptorIndex}
Attributes count: {AttributesCount}";
        }
    }
}
