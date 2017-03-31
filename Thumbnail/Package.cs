using System;
using System.IO;
using Thumbnail.Configuration;
using Builify;
using Utilities;

namespace Thumbnail {
    public class Core {
        private readonly string _packagePath = Config.PackagePath;
        private readonly string _magicWord = Config.MagicWord;
        private readonly string _tempPackagePath = Config.TempPackagePath;

        public Core() {
            LoadPackage();
        }

        private void LoadPackage() {
            var text = GetPackageDataAndReturnNullOnError();

            if (string.IsNullOrEmpty(text)) {
                return;
            }

            var realPackage = GetContentData(text);

            Zip.CreatePackage(_tempPackagePath, realPackage);
            Zip.ExtractFile(_tempPackagePath, Config.ManifestFileName);

            var manifest = Files.GetTemplateManifest(Config.ManifestFileName);

            Console.WriteLine(manifest.name);
            Console.WriteLine(manifest.version);

            Screenshots.Take(manifest);
        }

        private string GetPackageDataAndReturnNullOnError() {
            var text = string.Empty;

            try {
                text = File.ReadAllText(_packagePath);
            } catch (FileNotFoundException e) {
                Console.WriteLine($"'{e.FileName}' not found.");
            }

            return text;
        }

        private string GetContentData(string text) {
            return text
                .Remove(text.Length - 1)
                .Remove(0, (_magicWord.Length - 1) + 2);
        }
    }
}
