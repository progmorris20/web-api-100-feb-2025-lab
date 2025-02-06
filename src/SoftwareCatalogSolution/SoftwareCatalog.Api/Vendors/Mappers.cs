using Riok.Mapperly.Abstractions;

namespace SoftwareCatalog.Api.Vendors;


[Mapper]
public static partial class VendorMappers
{
    public static partial IQueryable<VendorDetailsResponseModel> ProjectToModel(this IQueryable<VendorEntity> entity);

    [MapProperty(nameof(VendorEntity.Slug), nameof(VendorDetailsResponseModel.Id))]
    [MapperIgnoreSource(nameof(VendorEntity.Id))]
    public static partial VendorDetailsResponseModel MapToModel(this VendorEntity entity);
}
