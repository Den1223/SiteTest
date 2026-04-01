using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SiteTest.Config;

namespace SiteTest.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TestConfig.DefaultTimeoutSeconds));
        }

        protected IWebElement FindElement(By locator) =>
            Wait.Until(d => d.FindElement(locator));

        protected IReadOnlyCollection<IWebElement> FindElements(By locator) =>
            Driver.FindElements(locator);

        protected void ClickViaJs(IWebElement element)
        {
            var js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);
            Wait.Until(_ =>
            {
                try
                {
                    js.ExecuteScript("arguments[0].click();", element);
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }

        protected void ClickViaJs(By locator) => ClickViaJs(FindElement(locator));

        protected void WaitForUrlContains(string fragment) =>
            Wait.Until(d => d.Url.Contains(fragment));

        protected void WaitForUrlNotContains(string fragment) =>
            Wait.Until(d => !d.Url.Contains(fragment));

        protected void WaitForElementsPresent(By locator) =>
            Wait.Until(d => d.FindElements(locator).Count > 0);

        protected void NavigateWithRetry(string url, By expectedElement, int maxAttempts = 3)
        {
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                Driver.Navigate().GoToUrl(url);
                try
                {
                    Wait.Until(d => d.FindElement(expectedElement));
                    return;
                }
                catch (WebDriverTimeoutException) when (attempt < maxAttempts - 1)
                {
                    Driver.Navigate().Refresh();
                }
            }
        }
    }
}