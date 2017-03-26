namespace Builify.Models.Template {
    public class Files {
        public string html { get; set; }
        public string stylesheet { get; set; }
        public string javascript { get; set; }
    }

    public class Output {
        public string dir { get; set; }
        public string name { get; set; }
    }

    public class Template {
        public string name { get; set; }
        public Files files { get; set; }
        public Output output { get; set; }
        public bool createThumbnails { get; set; }
    }
}
