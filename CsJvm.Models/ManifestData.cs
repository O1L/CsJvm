namespace CsJvm.Models
{
    /// <summary>
    /// JAR manifest data
    /// </summary>
    public class ManifestData
    {
        /// <summary>
        /// Manifest version
        /// </summary>
        public Version? ManifestVersion { get; set; }

        /// <summary>
        /// Implementation vendor name
        /// </summary>
        public string? ImplementationVendor { get; set; }

        /// <summary>
        /// Implementation title
        /// </summary>
        public string? ImplementationTitle { get; set; }

        /// <summary>
        /// Implementation version
        /// </summary>
        public Version? ImplementationVersion { get; set; }

        /// <summary>
        /// Specification vendor
        /// </summary>
        public string? SpecificationVendor { get; set; }

        /// <summary>
        /// Creator name
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Specification title
        /// </summary>
        public string? SpecificationTitle { get; set; }

        /// <summary>
        /// Specification version
        /// </summary>
        public Version? SpecificationVersion { get; set; }

        /// <summary>
        /// Main entry class
        /// </summary>
        public string? MainClass { get; set; }
    }
}
