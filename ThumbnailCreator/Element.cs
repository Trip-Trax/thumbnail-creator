using OpenQA.Selenium;
using System.Drawing;
using System.IO;
using System.Threading;

namespace ThumbnailCreator {
    public static class Element {
        public static Bitmap CropAtRect(this Bitmap b, Rectangle r) {
            Bitmap nb = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(nb);
            g.DrawImage(b, -r.X, -r.Y);
            return nb;
        }

        public static Bitmap GetScreenshot(IWebDriver driver, int elementHeight, int elementY) {
            // Get the size of the viewport
            var viewportWidth = (int)(long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.clientWidth");

            // Scroll
            string scrollCommand = $"window.scrollBy(0, {elementY})";
            ((IJavaScriptExecutor)driver).ExecuteScript(scrollCommand);

            Thread.Sleep(1000);

            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            Bitmap image = Image.FromStream(new MemoryStream(screenshot.AsByteArray)) as Bitmap;
            Bitmap croppedImage = CropAtRect(image, new Rectangle(0, 0, viewportWidth, elementHeight));

            return croppedImage;
        }
    }
}
