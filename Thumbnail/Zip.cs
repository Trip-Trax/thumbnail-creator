using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using Utilities;

namespace Thumbnail {
    public class Zip {
        public static void CreatePackage(string path, string data) {
            try {
                if (File.Exists(path)) {
                    File.Delete(path);
                }

                using (FileStream fs = File.Create(path)) {
                    var packageFileData = Base64.Decode(data);
                    fs.Write(packageFileData, 0, packageFileData.Length);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void ExtractFile(string packagePath, string fileName) {
            ZipFile zf = null;

            try {
                var fs = File.OpenRead(packagePath);

                zf = new ZipFile(fs);

                foreach (ZipEntry zipEntry in zf) {
                    if (!zipEntry.IsFile) {
                        continue; // Ignore directories
                    }

                    var entryFileName = zipEntry.Name;

                    if (entryFileName == fileName) {
                        var zipStream = zf.GetInputStream(zipEntry);
                        var fullZipToPath = fileName;
                        var buffer = new byte[4096];

                        using (var streamWriter = File.Create(fullZipToPath)) {
                            StreamUtils.Copy(zipStream, streamWriter, buffer);
                        }
                    }
                }
            } finally {
                if (zf != null) {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }
    }
}
