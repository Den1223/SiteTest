using FluentAssertions;

namespace SiteTest.Tests
{
    public class FavoriteTests : BaseTest
    {
        [Theory]
        [InlineData(TestData.ValidCredentials.Email1, TestData.ValidCredentials.Password)]
        [InlineData(TestData.ValidCredentials.Email2, TestData.ValidCredentials.Password)]
        [InlineData(TestData.ValidCredentials.Email3, TestData.ValidCredentials.Password)]
        public void UC2_AddFavorites_ShouldAppearOnFavoritesPage(string email, string password)
        {
            Logger.Info($"UC-2: Testing favorites with credentials: {email}");

            var loginPage = new LoginPage(driver);
            loginPage.Open();
            loginPage.LoginAndWaitForProducts(email, password);
            Logger.Info("Logged in successfully");

            var productsPage = new ProductsPage(driver);
            productsPage.WaitForProductsLoaded();

            var allProducts = productsPage.GetProductNames();
            Logger.Info($"Found {allProducts.Count} products on the page");

            productsPage.ClickFavoriteOnProduct(0);
            Logger.Info($"Favorited product at index 0: {allProducts[0]}");

            productsPage.ClickFavoriteOnProduct(1);
            Logger.Info($"Favorited product at index 1: {allProducts[1]}");

            var favoritedNames = new List<string> { allProducts[0], allProducts[1] };

            productsPage.OpenFavorites();
            Logger.Info("Navigated to Favorites page");

            var favoritesPage = new FavoritesPage(driver);
            favoritesPage.HasProducts().Should().BeTrue("Favorites page should contain products");

            var favoriteProducts = favoritesPage.GetFavoriteProductNames();
            Logger.Info($"Found {favoriteProducts.Count} products on Favorites page");

            foreach (var name in favoritedNames)
            {
                favoriteProducts.Should().Contain(name, $"'{name}' should appear on Favorites page");
                Logger.Info($"Verified '{name}' is on Favorites page");
            }

            Logger.Info("UC-2: Test passed — favorited items verified on Favorites page");
        }
    }
}
