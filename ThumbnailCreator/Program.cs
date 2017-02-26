using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using OpenQA.Selenium;
using System.Drawing;

namespace ThumbnailCreator {
    class Program {
        private static void TakeScreenShots(IWebDriver driver, List<IWebElement> elements) {
            for (int i = 0; i < 10; i++) {
                IWebElement currentElement = elements[i];
                int elementHeight = currentElement.Size.Height;
                int elementY = 0;

                if (i != 0) {
                    elementY = 0;

                    for (int j = 0; j < i; i++) {
                        elementY += elements[j].Size.Height;
                    }
                }

                Bitmap elementImage = Element.GetScreenshot(driver, elementHeight, elementY);
                elementImage.Save(@"C:\Users\Genert\Desktop\test\pic" + $"{i}" + ".jpg", ImageFormat.Jpeg);
            }
        }

        static void Main(string[] args) {
            Package pckg = new Package();

            Console.ReadKey();

            /*CoreDriver driver = new CoreDriver();

            driver.Initialize();

            var chromeInstance = driver.Driver;

            chromeInstance.Navigate().GoToUrl("http://localhost:3000/");
            chromeInstance.Manage().Window.Maximize();

            IReadOnlyCollection<IWebElement> allBodyElements = chromeInstance.FindElements(By.CssSelector("body > div"));
            List<IWebElement> parsedBodyElements = new List<IWebElement>();

            foreach (IWebElement element in allBodyElements) {
                if (element.GetAttribute("class") != "menu__wrapper") {
                    parsedBodyElements.Add(element);
                }
            }

            TakeScreenShots(chromeInstance, parsedBodyElements);

            chromeInstance.Quit();*/
        }
    }
}
