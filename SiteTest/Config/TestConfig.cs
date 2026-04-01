namespace SiteTest.Config
{
    public static class TestConfig
    {
        public static string BaseUrl =>
            Environment.GetEnvironmentVariable("TEST_BASE_URL") ?? "https://practice.qabrains.com/ecommerce";

        public static string LoginUrl => $"{BaseUrl}/login";

        public static string FavoritesUrl => $"{BaseUrl}/favorites";

        public static int DefaultTimeoutSeconds =>
            int.TryParse(Environment.GetEnvironmentVariable("TEST_TIMEOUT"), out var t) ? t : 30;

        public static int PageLoadTimeoutSeconds =>
            int.TryParse(Environment.GetEnvironmentVariable("TEST_PAGE_LOAD_TIMEOUT"), out var t) ? t : 30;
    }
}