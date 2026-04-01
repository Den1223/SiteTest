using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SiteTest
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private WebDriverWait Wait() => new(driver, TimeSpan.FromSeconds(30));
        private IWebElement EmailField =>
            Wait().Until(d => d.FindElement(By.XPath("//input[@id='email']")));

        private IWebElement PasswordField =>
            Wait().Until(d => d.FindElement(By.XPath("//input[@id='password']")));

        private IWebElement LoginButton =>
            Wait().Until(d => d.FindElement(By.XPath("//button[@type='submit']")));

        public string GetEmailErrorText() =>
            Wait().Until(d => d.FindElement(By.XPath("//p[contains(text(), 'Username is incorrect')]"))).Text;

        public string GetPasswordErrorText() =>
            Wait().Until(d => d.FindElement(By.XPath("//p[contains(text(), 'Password is incorrect')]"))).Text;

        public void Open()
        {
            for (int attempt = 0; attempt < 3; attempt++)
            {
                try
                {
                    driver.Navigate().GoToUrl("https://practice.qabrains.com/ecommerce/login");
                    Wait().Until(d => d.FindElement(By.XPath("//input[@id='email']")));
                    return;
                }
                catch (WebDriverTimeoutException) when (attempt < 2)
                {
                    driver.Navigate().Refresh();
                }
            }
        }

        public void Login(string email, string password)
        {
            EmailField.Clear();
            EmailField.SendKeys(email);
            PasswordField.Clear();
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }

        public void LoginAndWaitForProducts(string email, string password)
        {
            Login(email, password);
            Wait().Until(d => !d.Url.Contains("/login"));
        }

    }
}