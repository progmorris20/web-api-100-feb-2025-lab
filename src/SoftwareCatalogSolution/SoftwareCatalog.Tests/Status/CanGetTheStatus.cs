

using Alba;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using SoftwareCatalog.Api.Status;

namespace SoftwareCatalog.Tests.Status;
public class CanGetTheStatus
{

    [Fact]
    public async Task GetsA200WhenGettingTheStatus()
    {
        // Given

        var fakeDate = new DateTimeOffset(new DateTime(1969, 4, 20, 11, 59, 00));
        var fakeTimeProvider = new FakeTimeProvider(fakeDate);
        var expectedStatus = new StatusResponse(fakeDate, "Looks Good!");
        // This will start up our API, with our configuration (Program)
        var host = await AlbaHost.For<Program>(config =>
        {
            config.ConfigureServices(services =>
            {
                services.AddSingleton<TimeProvider>(_ => fakeTimeProvider);
            });
            //config.ConfigureAppConfiguration(ep =>
            //{
            //    // the hosting application // rarely used
            //});

        });

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/status");
            api.StatusCodeShouldBeOk();

        });
        var body = response.ReadAsJson<StatusResponse>();
        Assert.NotNull(body); // did we get a response?


        Assert.Equal(expectedStatus, body);

    }
}


//public class FakeTestingTimeProvider : IProvideTheSystemTime
//{
//    public DateTimeOffset GetSystemTime()
//    {
//        return new DateTimeOffset(new DateTime(1969, 4, 20, 11, 59, 00));
//    }
//}