using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ThumbnailCreator {
    public class ManifestFile {
        private static readonly string ManifestFilePath = Configuration.ManifestFileName;
        private static BuilifyTemplateManifest File { get; set; }

        public BuilifyTemplateManifest Get() {
            var manifestFileData = GetManifestFileData();

            File = SerializeJson(manifestFileData);

            return File;
        }

        private static string GetManifestFileData() {
            string data = string.Empty;

            try {
                if (System.IO.File.Exists(ManifestFilePath)) {
                    data = System.IO.File.ReadAllText(ManifestFilePath);
                }
            } catch (Exception ex) {
                throw ex;
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
