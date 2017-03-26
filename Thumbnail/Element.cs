using OpenQA.Selenium;
using System.Drawing;
using System.IO;
using OpenQA.Selenium.Interactions;
using Utilities;

namespace Thumbnail {
    public static class Element {
        public static Bitmap CropAtRect(this Bitmap b, Rectangle r) {
            var nb = new Bitmap(r.Width, r.Height);
            var g = Graphics.FromImage(nb);
            g.DrawImage(b, -r.X, -r.Y);
            return nb;
        }

        public static Bitmap GetScreenshot(IWebDriver driver, IWebElement element) {
            var viewportWidth = (int)(long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.clientWidth");
            var actions = new Actions(driver);
            var elementHeight = element.Size.Height;

            actions.MoveToElement(element);
            actions.Perform();

            Timer.Sleep(1000);

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            var image = Image.FromStream(new MemoryStream(screenshot.AsByteArray)) as Bitmap;
            var croppedImage = CropAtRect(image, new Rectangle(0, 0, viewportWidth, elementHeight));

            return ImageProcessingUtility.ScaleImage(croppedImage, 750, 400);
        }
    }
}
