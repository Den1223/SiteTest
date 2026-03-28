using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SiteTest
{
    public class FavoritesPage
    {
        private readonly IWebDriver driver;

        public FavoritesPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private WebDriverWait Wait() => new(driver, TimeSpan.FromSeconds(10));

        public List<string> GetFavoriteProductNames()
        {
            Wait().Until(d => d.FindElements(By.XPath("//a[contains(@class, 'text-lg')]")).Count > 0);
            return driver.FindElements(By.XPath("//a[contains(@class, 'text-lg')]"))
                .Select(e => e.Text)
                .Where(t => !string.IsNullOrEmpty(t))
                .ToList();
        }

        public bool HasProducts()
        {
            try
            {
                Wait().Until(d => d.FindElements(By.XPath("//a[contains(@class, 'text-lg')]")).Count > 0);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}