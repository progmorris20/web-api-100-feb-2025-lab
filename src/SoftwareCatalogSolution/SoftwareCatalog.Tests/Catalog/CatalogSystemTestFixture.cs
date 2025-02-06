


using Alba;

namespace SoftwareCatalog.Tests.Catalog;
public class CatalogSystemTestFixture : IAsyncLifetime
{
    public IAlbaHost Host { get; set; } = null!;
    public async Task InitializeAsync()
    {
        Host = await AlbaHost.For<Program>();
    }
    public async Task DisposeAsync()
    {
        await Host.DisposeAsync();
    }
}
