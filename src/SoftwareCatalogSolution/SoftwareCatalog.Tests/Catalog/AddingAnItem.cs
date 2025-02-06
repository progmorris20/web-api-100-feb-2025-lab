using SoftwareCatalog.Api.Catalog;

namespace SoftwareCatalog.Tests.Catalog;

[Trait("Category", "System")]
public class AddingAnItem(CatalogSystemTestFixture fixture) : IClassFixture<CatalogSystemTestFixture>
{

    [Theory]
    [InlineData("Visual Studio Code", "microsoft", CatalogItemLicenceTypes.OpenSource)]
    [InlineData("Visual Studio", "microsoft", CatalogItemLicenceTypes.Paid)]
    [InlineData("Bob", "microsoft", CatalogItemLicenceTypes.Free)]
    [InlineData("Destiny 2", "bungie", CatalogItemLicenceTypes.Paid)]
    public async Task CanAddAnItem(string name, string vendor, CatalogItemLicenceTypes license)
    {



        var itemToPost = new CatalogItemRequestModel
        {
            Name = name
        };
        var resource = $"/vendors/{vendor.ToLower()}/{license.ToString().ToLower()}";
        var response = await fixture.Host.Scenario(api =>
        {
            api.Post.Json(itemToPost).ToUrl(resource);
            api.StatusCodeShouldBe(201);
        });

        var responseFromThePost = response.ReadAsJson<CatalogItemResponseDetailsModel>();
        Assert.NotNull(responseFromThePost);

        //   Assert.Equal(expectedResponse, body);

        var getResponse = await fixture.Host.Scenario(api =>
        {
            api.Get.Url($"/catalog/{responseFromThePost.Id}");
        });

        var responseFromGet = getResponse.ReadAsJson<CatalogItemResponseDetailsModel>();
        Assert.NotNull(responseFromGet);

        Assert.Equal(responseFromThePost, responseFromGet);

    }


}
