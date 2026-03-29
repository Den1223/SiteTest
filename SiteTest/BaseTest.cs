using NLog;
using OpenQA.Selenium;

namespace SiteTest
{
    public class BaseTest : IDisposable
    {
        protected IWebDriver driver;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public BaseTest()
        {
            Logger.Info("Initializing WebDriver (Chrome)");
            driver = DriverFactory.CreateDriver("chrome");
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        public void Dispose()
        {
            Logger.Info("Quitting WebDriver");
            driver.Quit();
        }
    }
}
