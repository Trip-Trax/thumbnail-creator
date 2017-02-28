using System;
using System.IO;
using ThumbnailCreator.Model;
using ThumbnailCreator.Configuration;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace ThumbnailCreator {
    public class Package {
        private static string packagePath = @"C:\Users\Genert\Desktop\Projects\trip-trax\arkio\builder\arkio.builify.js";
        private const string MagicWord = "var __BUILIFY_TEMPLATE = ";
        private const string tempPackagePath = @"temp.zip";

        public Package() {
            LoadPackage();
        }

        private void LoadPackage() {
            var text = GetPackageData();
            var realPackage = GetContentData(text);

            Zip.CreatePackage(tempPackagePath, realPackage);
            Zip.ExtractFile(tempPackagePath, Config.ManifestFileName);

            ManifestFile manifest = new ManifestFile();
            BuilifyTemplateManifest result = manifest.Get();

            Console.WriteLine(result.name);
            Console.WriteLine(result.version);

            Screenshots.Take(result);
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
