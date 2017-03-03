using System;
using System.IO;
using Thumbnail.Configuration;

namespace Thumbnail {
    public class Core {
        private readonly string _packagePath = Config.PackagePath;
        private readonly string _magicWord = Config.MagicWord;
        private readonly string _tempPackagePath = Config.TempPackagePath;

        public Core() {
            LoadPackage();
        }

        private void LoadPackage() {
            var text = GetPackageData();
            var realPackage = GetContentData(text);

            Zip.CreatePackage(_tempPackagePath, realPackage);
            Zip.ExtractFile(_tempPackagePath, Config.ManifestFileName);

            var manifest = new ManifestFile();
            var result = manifest.Get();

            Console.WriteLine(result.name);
            Console.WriteLine(result.version);

            Screenshots.Take(result);
        }

        private string GetPackageData() {
            var text = File.ReadAllText(_packagePath);
            return text;
        }

        private string GetContentData(string text) {
            return text
                .Remove(text.Length - 1)
                .Remove(0, (_magicWord.Length - 1) + 2);
        }
    }
}
