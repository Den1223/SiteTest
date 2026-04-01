using FluentAssertions;
using SiteTest.Data;
using SiteTest.Pages;

namespace SiteTest.Tests
{
    public class FavoriteTestsFirefox : BaseTest
    {
        public FavoriteTestsFirefox() : base("firefox") { }

        [Theory]
        [InlineData(TestData.ValidCredentials.Email1, TestData.ValidCredentials.Password)]
        [InlineData(TestData.ValidCredentials.Email2, TestData.ValidCredentials.Password)]
        [InlineData(TestData.ValidCredentials.Email3, TestData.ValidCredentials.Password)]
        public void UC2_AddFavorites_ShouldAppearOnFavoritesPage_Firefox(string email, string password)
        {
            Logger.Info($"UC-2 [Firefox]: Testing favorites with credentials: {email}");

            var loginPage = new LoginPage(driver);
            loginPage.Open();
            loginPage.LoginAndWaitForProducts(email, password);

            var productsPage = new ProductsPage(driver);
            productsPage.WaitForProductsLoaded();

            var allProducts = productsPage.GetProductNames();

            productsPage.ClickFavoriteOnProduct(0);
            productsPage.ClickFavoriteOnProduct(1);

            var favoritedNames = new List<string> { allProducts[0], allProducts[1] };

            productsPage.OpenFavorites();

            var favoritesPage = new FavoritesPage(driver);
            var favoriteProducts = favoritesPage.GetFavoriteProductNames();

            foreach (var name in favoritedNames)
            {
                favoriteProducts.Should().Contain(name, $"'{name}' should appear on Favorites page");
            }

            Logger.Info("UC-2 [Firefox]: Test passed");
        }
    }
}