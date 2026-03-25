using OpenQA.Selenium;

public class LoginPage
{
    private readonly IWebDriver driver;

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    private IWebElement Email => driver.FindElement(By.XPath("//input[@id='email']"));
    private IWebElement Password => driver.FindElement(By.XPath("//input[@id='password']"));
    private IWebElement LoginButton => driver.FindElement(By.XPath("//button[contains(text(),'Login')]"));

    public void Open()
    {
        driver.Navigate().GoToUrl("https://practice.qabrains.com/ecommerce");
    }

    public void Login(string email, string password)
    {
        Email.SendKeys(email);
        Password.SendKeys(password);
        LoginButton.Click();
    }
}