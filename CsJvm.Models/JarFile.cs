namespace CsJvm.Models
{
    /// <summary>
    /// Java archive file
    /// </summary>
    public class JarFile
    {
        /// <summary>
        /// JAR file manifest data
        /// </summary>
        public ManifestData Manifest { get; set; } = new();

        /// <summary>
        /// A list of class files (full relative path)
        /// </summary>
        public string[] ClassFiles { get; set; } = Array.Empty<string>();
    }
}
