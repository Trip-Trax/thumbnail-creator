using System;
using System.IO;
using OpenQA.Selenium;
using System.Collections.Generic;
using ThumbnailCreator.Model;

namespace ThumbnailCreator {
    public class BlockElement {
        public IWebElement Element { get; set; } = null;
        public string FileName { get; set; } = string.Empty;
    }

    public static class Screenshots {
        private static CoreDriver _driver { get; set; } = null;

        private static void TakeScreenShots(IWebDriver driver, IReadOnlyCollection<BlockElement> elements) {
            foreach (var block in elements) {
                var elementImage = Element.GetScreenshot(driver, block.Element);
                var blockFileName = Path.GetFileName(block.FileName);
                var imageFileName = @"C:\Users\Genert\Desktop\test\" + $"{blockFileName}";

                elementImage.Save(imageFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
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
            driver.Navigate().GoToUrl("http://localhost:3000/");
            driver.Manage().Window.Maximize();
        }

        public static void Take(BuilifyTemplateManifest manifestObject) {
            _driver = new CoreDriver();
            _driver.Initialize();

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
    }
}
