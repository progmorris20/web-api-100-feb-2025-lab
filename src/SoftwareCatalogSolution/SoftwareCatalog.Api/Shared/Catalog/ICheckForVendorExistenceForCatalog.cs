namespace SoftwareCatalog.Api.Shared.Catalog;

public interface ICheckForVendorExistenceForCatalog
{
      Task<bool> DoesVendorExistAsync(string slug);
}
