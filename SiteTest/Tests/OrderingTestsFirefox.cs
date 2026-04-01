using FluentAssertions;
using SiteTest.Data;
using SiteTest.Pages;

namespace SiteTest.Tests
{
    public class OrderingTestsFirefox : BaseTest
    {
        public OrderingTestsFirefox() : base("firefox") { }

        [Theory]
        [InlineData(TestData.ValidCredentials.Email1, TestData.ValidCredentials.Password)]
        [InlineData(TestData.ValidCredentials.Email2, TestData.ValidCredentials.Password)]
        [InlineData(TestData.ValidCredentials.Email3, TestData.ValidCredentials.Password)]
        public void UC3_OrderProducts_LowToHigh_ShouldBeSortedByPrice_Firefox(string email, string password)
        {
            Logger.Info($"UC-3 [Firefox]: Testing product ordering with credentials: {email}");

            var loginPage = new LoginPage(driver);
            loginPage.Open();
            loginPage.LoginAndWaitForProducts(email, password);

            var productsPage = new ProductsPage(driver);
            productsPage.WaitForProductsLoaded();

            productsPage.SelectOrderBy("Low to High (Price)");

            var prices = productsPage.GetProductPrices();
            Logger.Info($"Prices: [{string.Join(", ", prices.Select(p => $"${p}"))}]");

            prices.Should().HaveCountGreaterThan(1, "There should be multiple products to compare");
            prices.Should().BeInAscendingOrder("Products should be sorted from low to high price");

            Logger.Info("UC-3 [Firefox]: Test passed");
        }
    }
}