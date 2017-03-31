using OpenQA.Selenium;

namespace Thumbnail {
    public class BlockElement {
        public IWebElement Element { get; set; } = null;
        public string FileName { get; set; } = string.Empty;
    }
}
