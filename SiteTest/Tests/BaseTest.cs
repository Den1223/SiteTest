using NLog;
using OpenQA.Selenium;
using SiteTest.Config;
using SiteTest.Drivers;

namespace SiteTest.Tests
{
    public class BaseTest : IDisposable
    {
        protected readonly IWebDriver driver;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private bool _disposed;

        protected BaseTest(string browser = "chrome")
        {
            Logger.Info($"Initializing WebDriver ({browser})");
            driver = DriverFactory.CreateDriver(browser);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(TestConfig.PageLoadTimeoutSeconds);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Logger.Info("Quitting WebDriver");
                driver.Quit();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}