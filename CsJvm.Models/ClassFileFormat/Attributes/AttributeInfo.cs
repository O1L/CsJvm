namespace CsJvm.Models.ClassFileFormat.Attributes
{
    /// <summary>
    /// Attribute Info
    /// </summary>
    public abstract class AttributeInfo
    {
        /// <summary>
        /// The constant_pool entry at attribute_name_index must be a CONSTANT_Utf8_info structure representing the name of the attribute.
        /// </summary>
        public ushort AttributeNameIndex { get; set; }

        /// <summary>
        /// The length of the subsequent information in bytes
        /// </summary>
        public uint AttributeLength { get; set; }

        /// <summary>
        /// Attribute name
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
