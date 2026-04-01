using FluentAssertions;

namespace SiteTest.Tests
{
    public class OrderingTests : BaseTest
    {
        [Theory]
        [InlineData(TestData.ValidCredentials.Email1, TestData.ValidCredentials.Password)]
        [InlineData(TestData.ValidCredentials.Email2, TestData.ValidCredentials.Password)]
        [InlineData(TestData.ValidCredentials.Email3, TestData.ValidCredentials.Password)]
        public void UC3_OrderProducts_LowToHigh_ShouldBeSortedByPrice(string email, string password)
        {
            Logger.Info($"UC-3: Testing product ordering with credentials: {email}");

            var loginPage = new LoginPage(driver);
            loginPage.Open();
            loginPage.LoginAndWaitForProducts(email, password);
            Logger.Info("Logged in successfully");

            var productsPage = new ProductsPage(driver);
            productsPage.WaitForProductsLoaded();
            Logger.Info("Products loaded");

            productsPage.SelectOrderBy("Low to High (Price)");
            Logger.Info("Selected 'Low to High (Price)' ordering");

            var prices = productsPage.GetProductPrices();
            Logger.Info($"Retrieved {prices.Count} product prices: [{string.Join(", ", prices.Select(p => $"${p}"))}]");

            prices.Should().HaveCountGreaterThan(1, "There should be multiple products to compare");
            prices.Should().BeInAscendingOrder("Products should be sorted from low to high price");

            Logger.Info("UC-3: Test passed — products are sorted by price (low to high)");
        }
    }
}