using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace ThumbnailCreator {
    public class CoreDriver {
        private OpenQA.Selenium.IWebDriver _driver;

        private string _defaultProfileDir = @"C:\Users\Genert\AppData\Local\Google\Chrome\User Data\Default";

        public void Initialize() {
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();

            Console.WriteLine($"user-data-dir={_defaultProfileDir}");

            options.AddArgument($"user-data-dir={_defaultProfileDir}");
            options.AddArgument("--disable-plugins");

            _driver = new ChromeDriver(service, options, TimeSpan.FromMinutes(5));
        }

        public IWebDriver Driver => _driver;
    }
}
