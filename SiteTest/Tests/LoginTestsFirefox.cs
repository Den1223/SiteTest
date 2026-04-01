using FluentAssertions;
using SiteTest.Data;
using SiteTest.Pages;

namespace SiteTest.Tests
{
    public class LoginTestsFirefox : BaseTest
    {
        public LoginTestsFirefox() : base("firefox") { }

        [Theory]
        [ClassData(typeof(InvalidCredentialsData))]
        public void UC1_Login_WithWrongCredentials_ShouldShowErrors_Firefox(string email, string password)
        {
            Logger.Info($"UC-1 [Firefox]: Testing login with wrong credentials: {email} / {password}");

            var loginPage = new LoginPage(driver);
            loginPage.Open();
            loginPage.Login(email, password);

            var emailError = loginPage.GetEmailErrorText();
            var passwordError = loginPage.GetPasswordErrorText();

            Logger.Info($"Email error: '{emailError}', Password error: '{passwordError}'");

            emailError.Should().Contain("incorrect", "Email error message should contain 'incorrect'");
            passwordError.Should().Contain("incorrect", "Password error message should contain 'incorrect'");

            Logger.Info("UC-1 [Firefox]: Test passed");
        }
    }
}