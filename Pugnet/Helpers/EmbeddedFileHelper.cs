using System.IO.Compression;
using System.Reflection;

namespace Pugnet.Helpers;

public static class EmbeddedFileHelper
{
    private static readonly object _lock = new object();

    public static void ExpandEmbeddedFiles(string tempDirectory)
    {
        lock (_lock)
        {
            var assembly = Assembly.Load(new AssemblyName("Pugnet"));
            var embeddedResourceName = assembly.GetManifestResourceNames().First(resource => resource.Contains("embeddedNodeResources"));

            using (var stream = assembly.GetManifestResourceStream(embeddedResourceName))
            {
                var archive = new ZipArchive(stream, ZipArchiveMode.Read, false);
                var tempDir = new DirectoryInfo(tempDirectory);
                foreach (var entry in archive.Entries)
                {
                    var filePath = Path.Combine(tempDir.FullName, entry.FullName);
                    if (File.Exists(filePath))
                    {
                        continue;
                    }

                    Directory.CreateDirectory(new FileInfo(filePath).DirectoryName);
                    entry.ExtractToFile(filePath, true);
                }
            }
        }
    }
}
