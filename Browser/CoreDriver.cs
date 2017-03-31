using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Browser {
    public class CoreDriver {
        private IWebDriver _driver;
        private string _defaultProfileDir = string.Empty;

        public CoreDriver(string path) {
            _defaultProfileDir = path;
            Initialize();
        }

        public IWebDriver Driver {
            get {
                return _driver;
            }
        }

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
