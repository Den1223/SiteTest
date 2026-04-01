using FluentAssertions;

namespace SiteTest.Tests
{
    public class LoginTests : BaseTest
    {
        [Theory]
        [ClassData(typeof(InvalidCredentialsData))]
        public void UC1_Login_WithWrongCredentials_ShouldShowErrors(string email, string password)
        {
            Logger.Info($"UC-1: Testing login with wrong credentials: {email} / {password}");

            var loginPage = new LoginPage(driver);
            loginPage.Open();
            Logger.Info("Login page opened");

            loginPage.Login(email, password);
            Logger.Info("Submitted login form with invalid credentials");

            var emailError = loginPage.GetEmailErrorText();
            var passwordError = loginPage.GetPasswordErrorText();

            Logger.Info($"Email error: '{emailError}', Password error: '{passwordError}'");

            emailError.Should().Contain("incorrect", "Email error message should contain 'incorrect'");
            passwordError.Should().Contain("incorrect", "Password error message should contain 'incorrect'");

            Logger.Info("UC-1: Test passed — error messages verified");
        }
    }
}