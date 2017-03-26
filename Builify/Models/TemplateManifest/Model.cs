using System.Collections.Generic;

namespace Builify.Models.TemplateManifest {
    public class Core {
        public string javascript { get; set; }
        public string stylesheet { get; set; }
    }

    public class Core2 {
        public string type { get; set; }
        public string src { get; set; }
    }

    public class External {
        public List<Core2> core { get; set; }
    }

    public class Features {
        public bool videoBackground { get; set; }
        public bool imageBackground { get; set; }
        public bool colorBackground { get; set; }
        public bool countdown { get; set; }
        public bool formInput { get; set; }
    }

    public class Dev {
        public string source { get; set; }
    }

    public class Item {
        public Features features { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string thumbnail { get; set; }
        public string source { get; set; }
        public string query { get; set; }
        public Dev dev { get; set; }
    }

    public class Block {
        public string type { get; set; }
        public List<Item> items { get; set; }
    }

    public class Colors {
        public string body { get; set; }
        public string a { get; set; }
        public string p { get; set; }
        public string h1 { get; set; }
        public string h2 { get; set; }
        public string h3 { get; set; }
        public string h4 { get; set; }
        public string h5 { get; set; }
        public string h6 { get; set; }
        public string blockquote { get; set; }
        public string b { get; set; }
        public string i { get; set; }
        public string em { get; set; }
    }

    public class Size {
        public int basefont { get; set; }
        public double baseline { get; set; }
    }

    public class Typography {
        public Size size { get; set; }
    }

    public class Design {
        public Colors colors { get; set; }
        public Typography typography { get; set; }
    }

    public class Generated {
        public long _time { get; set; }
        public string _platform { get; set; }
        public string _architecture { get; set; }
    }

    public class TemplateManifest {
        public string name { get; set; }
        public Core core { get; set; }
        public External external { get; set; }
        public List<Block> blocks { get; set; }
        public Design design { get; set; }
        public Generated _generated { get; set; }
        public string version { get; set; }
    }
}
