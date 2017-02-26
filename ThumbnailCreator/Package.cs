using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace ThumbnailCreator {
    public class Package {
       
        private const string MagicWord = "var __BUILIFY_TEMPLATE = ";
        private static string tempPackagePath = @"temp.zip";

        public Package() {
            LoadPackage();
        }

        private void LoadPackage() {
            var text = GetPackageData();
            var realPackage = GetContentData(text);

            Zip.CreatePackage(tempPackagePath, realPackage);
            Zip.ExtractFile(tempPackagePath, Configuration.ManifestFileName);

            ManifestFile manifest = new ManifestFile();
            BuilifyTemplateManifest result = manifest.Get();

            Console.WriteLine(result.name);
            Console.WriteLine(result.version);

            foreach (Block block in result.blocks) {
                foreach (Item item in block.items) {
                    Console.WriteLine(item.title);
                }
            }
        }

        private string GetPackageData() {
            var text = File.ReadAllText(packagePath);
            return text;
        }

        private string GetContentData(string text) {
            return text
                .Remove(text.Length - 1)
                .Remove(0, (MagicWord.Length - 1) + 2);
        }
    }
}
