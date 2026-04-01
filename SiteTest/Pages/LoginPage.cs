using OpenQA.Selenium;
using SiteTest.Config;

namespace SiteTest.Pages
{
    public class LoginPage : BasePage
    {
        private static readonly By EmailFieldLocator = By.XPath("//input[@id='email']");
        private static readonly By PasswordFieldLocator = By.XPath("//input[@id='password']");
        private static readonly By LoginButtonLocator = By.XPath("//button[@type='submit']");
        private static readonly By EmailErrorLocator = By.XPath("//p[contains(text(), 'Username is incorrect')]");
        private static readonly By PasswordErrorLocator = By.XPath("//p[contains(text(), 'Password is incorrect')]");

        public LoginPage(IWebDriver driver) : base(driver) { }

        private IWebElement EmailField => FindElement(EmailFieldLocator);
        private IWebElement PasswordField => FindElement(PasswordFieldLocator);
        private IWebElement LoginButton => FindElement(LoginButtonLocator);

        public string GetEmailErrorText() => FindElement(EmailErrorLocator).Text;

        public string GetPasswordErrorText() => FindElement(PasswordErrorLocator).Text;

        public void Open()
        {
            NavigateWithRetry(TestConfig.LoginUrl, EmailFieldLocator);
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
            WaitForUrlNotContains("/login");
        }
    }
}