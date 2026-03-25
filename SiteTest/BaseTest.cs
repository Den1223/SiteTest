using OpenQA.Selenium;
using System;

namespace SiteTest
{
    internal class BaseTest : IDisposable
    {
        protected IWebDriver driver;

        public BaseTest()
        {
            driver = DriverFactory.CreateDriver("chrome");
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
