using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Builify.Json {
    public class JsonLoader<T> {
        private string _path = string.Empty;
        private string _fileData = string.Empty;
        private T _data = default(T);

        public JsonLoader(string path) {
            _path = path;

            GetFile();
            SerializeJson();
        }

        public T GetData() {
            return _data;
        }

        private void GetFile() {
            try {
                if (File.Exists(_path)) {
                    _fileData = File.ReadAllText(_path);
                } else {
                    throw new FileNotFoundException();
                }
            } catch (FileNotFoundException e) {
                throw new Exception($"'{e.FileName}' not found.");
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        private void SerializeJson() {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(_fileData));

            _data = (T)serializer.ReadObject(ms);
        }
    }
}
