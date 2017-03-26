using Builify.Json;
using Builify.Models.Template;
using Builify.Models.TemplateManifest;

namespace Builify {
    public static class Files {
        public static Template GetTemplateConfiguration(string path) {
            var templateJson = new JsonLoader<Template>(path);
            return templateJson.GetData();
        }

        public static TemplateManifest GetTemplateManifest(string path) {
            var templateJson = new JsonLoader<TemplateManifest>(path);
            return templateJson.GetData();
        }
    }
}
