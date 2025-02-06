using Riok.Mapperly.Abstractions;

namespace SoftwareCatalog.Api.Catalog;


//  // a -> b (CatalogItemRequestModel -> CatalogItemEntity)
public static class CatalogMappingExtensions
{
    public static CatalogItemEntity ToCatalogItemEntity(this CatalogItemRequestModel model, string vendor, CatalogItemLicenceTypes license)
    {
        return new CatalogItemEntity
        {
            Id = Guid.NewGuid(), // maybe you want comb guids in the future?
            Licence = license,
            Name = model.Name,
            Vendor = vendor
        };
    }

    //public static CatalogItemResponseDetailsModel ToCatalogDetailsResponseModel(this CatalogItemEntity entity)
    //{
    //    return new CatalogItemResponseDetailsModel
    //    {
    //        Id = entity.Id,
    //        Licence = entity.Licence,
    //        Name = entity.Name,
    //        Vendor = entity.Vendor
    //    };
    //}
}


[Mapper]
public static partial class CatalogMappers
{
    public static partial IQueryable<CatalogItemResponseDetailsModel> ProjectToDetailsModel(this IQueryable<CatalogItemEntity> model);
    public static partial CatalogItemResponseDetailsModel ToCatalogItemResponseModel(this CatalogItemEntity model);
}