namespace SiteTest.Data
{
    public static class TestData
    {
        public static class ValidCredentials
        {
            public const string Email1 = "test@qabrains.com";
            public const string Email2 = "practice@qabrains.com";
            public const string Email3 = "student@qabrains.com";
            public const string Password = "Password123";
        }
    }

    public class InvalidCredentialsData : TheoryData<string, string>
    {
        public InvalidCredentialsData()
        {
            Add("wronguser@test.com", "WrongPass1");
            Add("invalid@email.com", "123456");
            Add("notregistered@example.com", "Password999");
        }
    }

    public class BrowserData : TheoryData<string>
    {
        public BrowserData()
        {
            Add("chrome");
            Add("firefox");
        }
    }
}
