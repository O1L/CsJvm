using CsJvm.Abstractions.Loader;
using CsJvm.Loader.Parsers;
using CsJvm.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.IO.Compression;

namespace CsJvm.Loader
{
    /// <summary>
    /// JAR files loader
    /// </summary>
    public class JarLoader : IJarLoader
    {
        /// <summary>
        /// Main logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// JavaClass loader instance
        /// </summary>
        private readonly IJavaClassLoader _javaClassLoader;

        /// <summary>
        /// Flag to check the dispose state
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Main JAR file
        /// </summary>
        private JarFile? _jarFile;

        /// <summary>
        /// JAR archive handle
        /// </summary>
        private ZipArchive? _archive;

        // <summary>
        /// Loaded JavaClasses cache
        /// </summary>
        private readonly ConcurrentDictionary<string, JavaClass> _classesCache = new();

        /// <summary>
        /// ZIP entries cache
        /// </summary>
        private readonly ConcurrentDictionary<string, ZipArchiveEntry> _classFileEntries = new();

        /// <summary>
        /// Loaded JAR file
        /// </summary>
        public JarFile? JAR => _jarFile;

        public JarLoader(ILogger<JarLoader> logger, IJavaClassLoader javaClassLoader)
        {
            _logger = logger;
            _javaClassLoader = javaClassLoader;
        }

        /// <inheritdoc/>
        public async Task<bool> OpenAsync(string path)
            => await OpenJarAsync(path) != null;

        /// <inheritdoc/>
        public async Task<JavaClass?> GetClassAsync(string className)
        {
            JavaClass? javaClass = null;

            // check if class not found in specified jar
            if (!_classFileEntries.TryGetValue(className, out var entry))
                return null;

            // check already parsed class in cache
            if (!_classesCache.TryGetValue(className, out javaClass))
            {
                // load class
                using var stream = entry.Open();
                javaClass = await _javaClassLoader.LoadAsync(stream, className);
                if (stream == null || javaClass == null)
                    return null;
            }

            // check and load super-class (recursively)
            if (javaClass.SuperClass != null)
            {
                var superClass = await GetClassAsync(javaClass.SuperClass.Name);
                if (superClass != null)
                {
                    javaClass.SuperClass = superClass;
                    javaClass.SuperClass?.Implementations.Add(javaClass);
                }
            }

            // update cache
            //_classesCache.TryAdd(className, javaClass);

            return javaClass;
        }

        /// <summary>
        /// Open, parse and keep JAR file handle blocked
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns><see langword="true"></see> if jar found and loaded; otherwise <see langword="false"></see></returns>
        private Task<JarFile?> OpenJarAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path) || !".jar".Equals(Path.GetExtension(path), StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogWarning("Cannot open .jar from path = '{path}'", path);
                return Task.FromResult<JarFile?>(null);
            }

            try
            {
                // open jar as a zip archive and keep it opened
                _archive = ZipFile.OpenRead(path);

                // load only .class files
                foreach (var entry in _archive.Entries)
                {
                    if (!entry.FullName.EndsWith(".class", StringComparison.OrdinalIgnoreCase))
                        continue;

                    var className = Path.ChangeExtension(entry.FullName, null);
                    _classFileEntries[className] = entry;
                }

                _jarFile = new JarFile
                {
                    ClassFiles = _classFileEntries.Keys.ToArray()
                };

                // read manifest data
                using var stream = _archive.GetEntry("META-INF/MANIFEST.MF")?.Open();
                if (stream == null)
                    return Task.FromResult<JarFile?>(null);

                using var reader = new StreamReader(stream);
                return _jarFile.ParseManifestAsync(reader);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to open jar file from path = '{path}'", path);
                return Task.FromResult<JarFile?>(null);
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes manager resources
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _archive?.Dispose();

            _disposed = true;
        }
    }
}
