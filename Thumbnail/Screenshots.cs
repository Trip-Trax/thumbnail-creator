using System.IO;
using OpenQA.Selenium;
using System.Collections.Generic;
using Thumbnail.Configuration;
using Builify.Models.TemplateManifest;
using Browser;
using OpenQA.Selenium.Internal;
using System.Linq;

namespace Thumbnail {
    public static class Screenshots {
        private static CoreDriver _driver { get; set; } = null;

        public static void Take(TemplateManifest manifestObject) {
            _driver = new CoreDriver(Config.ChromeBrowserUserProfilePath);

            var chromeInstance = _driver.Driver;

            InitializeBrowser(chromeInstance);
            
            var elementQueries = new List<BlockElement>();

            foreach (var block in manifestObject.blocks) {
                foreach (var item in block.items) {
                    var blockItem = new BlockElement {
                        Element = chromeInstance.FindElement(By.CssSelector(item.query)),
                        FileName = item.thumbnail
                    };

                    elementQueries.Add(blockItem);
                }
            }

            TakeScreenShots(chromeInstance, elementQueries);

            CloseBrowser();
        }

        private static IWebElement SetAttribute(this IWebElement element, string name, string value) {
            var driver = ((IWrapsDriver)element).WrappedDriver;
            var jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", element, name, value);

            return element;
        }

        private static void TakeScreenShots(IWebDriver driver, IReadOnlyCollection<BlockElement> elements) {
            var currentBlockIndex = 0;
            var previousBlockIndex = currentBlockIndex;

            // Set body color to dark
            SetAttribute(_driver.Driver.FindElement(By.CssSelector("html")), "style", "background: #111;");

            // First, set all elements hidden
            foreach (var hiddenBlock in elements) {
                SetAttribute(hiddenBlock.Element, "style", "display: none;");
            }

            foreach (var block in elements) {
                // Hide previous element
                if (currentBlockIndex != 0) {
                    SetAttribute(elements.ElementAt(previousBlockIndex).Element, "style", "display: none;");
                }

                // Then show current element
                SetAttribute(block.Element, "style", "display: block;");

                // Take picture
                var elementImage = Element.GetScreenshot(driver, block.Element);
                var blockFileName = Path.GetFileName(block.FileName);
                var imageFileName = Config.ImagesTargetPath + $"{blockFileName}";

                // Save picture
                elementImage.Save(imageFileName, System.Drawing.Imaging.ImageFormat.Jpeg);

                // Move indexes
                previousBlockIndex = currentBlockIndex;
                currentBlockIndex += 1;
            }
        }

        private static void CloseBrowser() {
            _driver?.Driver.Quit();
        }

        private static List<IWebElement> GetElementsToScreenshot(IWebDriver driver) {
            var items = new List<IWebElement>();
            var elements = driver.FindElements(By.CssSelector("body > div"));

            foreach (var element in elements) {
                if (element.GetAttribute("class") != "menu__wrapper") {
                    items.Add(element);
                }
            }

            return items;
        }

        private static void InitializeBrowser(IWebDriver driver) {
            driver.Navigate().GoToUrl(Config.Server);
            driver.Manage().Window.Maximize();
        }
    }
}
