using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SiteTest
{
    public static class DriverFactory
    {
        // for another browser
        public static IWebDriver CreateDriver(string browser)
        {
            if (browser == "firefox")
            {
                var options = new FirefoxOptions();
                options.AcceptInsecureCertificates = true;
                return new FirefoxDriver(options);
            }

            var chromeOptions = new ChromeOptions();
            chromeOptions.AcceptInsecureCertificates = true;

            return new ChromeDriver(chromeOptions);
        }
    }
}
