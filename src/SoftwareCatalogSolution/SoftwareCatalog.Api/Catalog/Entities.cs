namespace SoftwareCatalog.Api.Catalog;
public enum CatalogItemLicenceTypes { OpenSource, Free, Paid }
public class CatalogItemEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Vendor { get; set; } = string.Empty;
    public CatalogItemLicenceTypes Licence { get; set; }
}