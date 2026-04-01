using System.Globalization;
using OpenQA.Selenium;
using System.Globalization;
using SiteTest.Config;

namespace SiteTest.Pages
{
    public class ProductsPage : BasePage
    {
        private static readonly By ProductPriceLocator = By.XPath("//span[starts-with(text(), '$')]");
        private static readonly By ProductNameLocator = By.XPath("//a[contains(@class, 'text-lg')]");
        private static readonly By FavoriteButtonLocator = By.XPath("//span[contains(@class, 'absolute') and contains(@class, 'top-3')]//button");
        private static readonly By OrderByDropdownLocator = By.XPath("//p[text()='Order by']/..//button");

        public ProductsPage(IWebDriver driver) : base(driver) { }

        public void WaitForProductsLoaded() => WaitForElementsPresent(ProductPriceLocator);

        public void ClickFavoriteOnProduct(int index)
        {
            var buttons = FindElements(FavoriteButtonLocator).ToList();
            if (index < buttons.Count)
            {
                ClickViaJs(buttons[index]);
                WaitForFavoriteAdded(index);
            }
        }

        public void SelectOrderBy(string option)
        {
            ClickViaJs(OrderByDropdownLocator);

            var optionLocator = By.XPath($"//div[contains(text(), '{option}')]");
            var optionElement = FindElement(optionLocator);
            ClickViaJs(optionElement);

            WaitForProductsReordered();
        }

        public void OpenFavorites()
        {
            Driver.Navigate().GoToUrl(TestConfig.FavoritesUrl);
            WaitForUrlContains("/favorites");
        }

        public List<string> GetProductNames()
        {
            WaitForProductsLoaded();
            return FindElements(ProductNameLocator)
                .Select(e => e.Text)
                .Where(t => !string.IsNullOrEmpty(t))
                .ToList();
        }

        public List<decimal> GetProductPrices()
        {
            WaitForProductsLoaded();
            return FindElements(ProductPriceLocator)
                .Select(e => e.Text)
                .Where(t => !string.IsNullOrEmpty(t))
                .Select(t => decimal.Parse(t.Replace("$", ""), CultureInfo.InvariantCulture))
                .ToList();
        }

        private void WaitForFavoriteAdded(int index)
        {
            Wait.Until(_ =>
            {
                var buttons = FindElements(FavoriteButtonLocator).ToList();
                if (index >= buttons.Count) return false;
                var style = buttons[index].GetAttribute("style") ?? "";
                return style.Contains("255, 0, 0");
            });
        }

        private void WaitForProductsReordered()
        {
            Wait.Until(d => d.Url.Contains("order_by"));
        }
    }
}