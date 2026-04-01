using OpenQA.Selenium;

namespace SiteTest.Pages
{
    public class FavoritesPage : BasePage
    {
        private static readonly By FavoriteProductNameLocator = By.XPath("//a[contains(@class, 'text-lg')]");

        public FavoritesPage(IWebDriver driver) : base(driver) { }

        public List<string> GetFavoriteProductNames()
        {
            WaitForElementsPresent(FavoriteProductNameLocator);
            return FindElements(FavoriteProductNameLocator)
                .Select(e => e.Text)
                .Where(t => !string.IsNullOrEmpty(t))
                .ToList();
        }
    }
}