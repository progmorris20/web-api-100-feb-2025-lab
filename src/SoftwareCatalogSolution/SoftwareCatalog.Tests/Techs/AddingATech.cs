using Alba;
using Alba.Security;
using SoftwareCatalog.Api.Techs;
using System.Security.Claims;


namespace SoftwareCatalog.Tests.Techs;
[Trait("Category", "System")]
public class AddingATech
{
    [Fact]
    public async Task CanAddATech()
    {
        var fakeIdentity = new AuthenticationStub().WithName("babs")
            .With(new Claim(ClaimTypes.Role, "manager"))
            .With(new Claim(ClaimTypes.Role, "software-center"));

        var host = await AlbaHost.For<Program>(fakeIdentity);

        var requestModel = new TechCreateModel
        {
            Name = "Nicky Momo",
            Contact = "nickmorris@fake.com"
        };

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestModel).ToUrl("/techs");
            api.StatusCodeShouldBe(201);
        });

        var location = postResponse.Context.Response.Headers.Location.ToString();

        var postBody = postResponse.ReadAsJson<TechDetailsResponseModel>();

        Assert.NotNull(postBody);

        var getResponse = await host.Scenario(api =>
        {
            api.Get.Url(location);
        });

        var getBody = getResponse.ReadAsJson<TechDetailsResponseModel>();
        Assert.NotNull(getBody);

        //var ts = postBody.CreatedOn - getBody.CreatedOn;

        //Assert.Equal(postBody, getBody);

    }

}
