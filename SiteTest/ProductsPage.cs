using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SiteTest
{
    public class ProductsPage
    {
        private readonly IWebDriver driver;

        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private WebDriverWait Wait() => new(driver, TimeSpan.FromSeconds(30));

        private IReadOnlyCollection<IWebElement> FavoriteButtons =>
            driver.FindElements(By.XPath("//span[contains(@class, 'absolute') and contains(@class, 'top-3')]//button"));

        public void WaitForProductsLoaded()
        {
            Wait().Until(d => d.FindElements(By.XPath("//span[starts-with(text(), '$')]")).Count > 0);
        }

        public void ClickFavoriteOnProduct(int index)
        {
            var js = (IJavaScriptExecutor)driver;
            var buttons = FavoriteButtons.ToList();
            if (index < buttons.Count)
            {
                js.ExecuteScript("arguments[0].scrollIntoView(true);", buttons[index]);
                js.ExecuteScript("arguments[0].click();", buttons[index]);
            }
        }

        public void SelectOrderBy(string option)
        {
            var js = (IJavaScriptExecutor)driver;

            var dropdown = Wait().Until(d =>
                d.FindElement(By.XPath("//p[text()='Order by']/..//button")));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", dropdown);
            js.ExecuteScript("arguments[0].click();", dropdown);

            var optionElement = Wait().Until(d => d.FindElement(By.XPath($"//div[contains(text(), '{option}')]")));
            js.ExecuteScript("arguments[0].click();", optionElement);
            Thread.Sleep(300);
        }

        public void OpenFavorites()
        {
            driver.Navigate().GoToUrl("https://practice.qabrains.com/ecommerce/favorites");
        }

        public List<string> GetProductNames()
        {
            WaitForProductsLoaded();
            return driver.FindElements(By.XPath("//a[contains(@class, 'text-lg')]"))
                .Select(e => e.Text)
                .Where(t => !string.IsNullOrEmpty(t))
                .ToList();
        }

        public List<decimal> GetProductPrices()
        {
            WaitForProductsLoaded();
            return driver.FindElements(By.XPath("//span[starts-with(text(), '$')]"))
                .Select(e => e.Text)
                .Where(t => !string.IsNullOrEmpty(t))
                .Select(t => decimal.Parse(t.Replace("$", ""), CultureInfo.InvariantCulture))
                .ToList();
        }
    }
}