using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Thumbnail.Configuration;

namespace Thumbnail {
    public class CoreDriver {
        private IWebDriver _driver;
        private readonly string _defaultProfileDir = Config.ChromeBrowserUserProfilePath;

        public IWebDriver Driver => _driver;

        public void Initialize() {
            var options = new ChromeOptions();
            var service = ChromeDriverService.CreateDefaultService();

            Console.WriteLine($"user-data-dir={_defaultProfileDir}");

            options.AddArgument($"user-data-dir={_defaultProfileDir}");
            options.AddArgument("--disable-plugins");

            _driver = new ChromeDriver(service, options, TimeSpan.FromMinutes(5));
        }
    }
}
