using System;
using System.Runtime.Serialization.Json;
using System.Text;
using Thumbnail.Model;
using Thumbnail.Configuration;

namespace Thumbnail {
    public class ManifestFile {
        private static readonly string _manifestFilePath = Config.ManifestFileName;
        private static BuilifyTemplateManifest File { get; set; }

        public BuilifyTemplateManifest Get() {
            var manifestFileData = GetManifestFileData();

            File = SerializeJson(manifestFileData);

            return File;
        }

        private static string GetManifestFileData() {
            var data = string.Empty;

            try {
                if (System.IO.File.Exists(_manifestFilePath)) {
                    data = System.IO.File.ReadAllText(_manifestFilePath);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            return data;
        }

        private static BuilifyTemplateManifest SerializeJson(string fileContents) {
            var serializer = new DataContractJsonSerializer(typeof(BuilifyTemplateManifest));
            var ms = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(fileContents));
            var result = (BuilifyTemplateManifest)serializer.ReadObject(ms);

            return result;
        }
    }
}
