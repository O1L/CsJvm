using CsJvm.Models;

namespace CsJvm.Loader.Parsers
{
    /// <summary>
    /// JAR manifest parser
    /// </summary>
    public static class ManifestParser
    {
        /// <summary>
        /// Parses manifest data
        /// </summary>
        /// <param name="jar">Jar file to parse</param>
        /// <param name="reader">Stream reader</param>
        /// <returns><see cref="JarFile"/> with parsed manifest data</returns>
        public static JarFile ParseManifest(this JarFile jar, StreamReader reader)
        {
            var line = reader.ReadLine();
            while (line != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    line = reader.ReadLine();
                    continue;
                }

                var pair = line.Split(':', StringSplitOptions.None);
                if (pair.Length == 2)
                {
                    var key = pair[0].Trim().ToLowerInvariant();
                    var value = pair[1].Trim();
                    switch (key)
                    {
                        case "manifest-version":
                            {
                                if (Version.TryParse(value, out var result))
                                    jar.Manifest.ManifestVersion = result;
                            }
                            break;

                        case "implementation-vendor":
                            jar.Manifest.ImplementationVendor = value;
                            break;

                        case "implementation-title":
                            jar.Manifest.ImplementationTitle = value;
                            break;

                        case "implementation-version":
                            {
                                var version = value.Replace('_', '.');
                                if (version.Contains('-'))
                                    version = version[..version.IndexOf('-')];

                                if (Version.TryParse(version, out var result))
                                    jar.Manifest.ImplementationVersion = result;
                            }
                            break;

                        case "specification-vendor":
                            jar.Manifest.SpecificationVendor = value;
                            break;

                        case "created-by":
                            jar.Manifest.CreatedBy = value;
                            break;

                        case "specification-title":
                            jar.Manifest.SpecificationTitle = value;
                            break;

                        case "specification-version":
                            {
                                if (Version.TryParse(value, out var result))
                                    jar.Manifest.SpecificationVersion = result;
                            }
                            break;

                        case "main-class":
                            jar.Manifest.MainClass = value;
                            break;

                        default:
                            break;
                    }
                }

                line = reader.ReadLine();
            }

            return jar;
        }
    }
}
