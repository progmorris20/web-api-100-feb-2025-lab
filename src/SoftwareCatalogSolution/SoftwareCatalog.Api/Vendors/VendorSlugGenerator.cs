
using Marten;
using Slugify;
using SoftwareCatalog.Api.Shared.Catalog;

namespace SoftwareCatalog.Api.Vendors;

public class VendorSlugGenerator(ICheckForUniqueVendorSlugs uniqueSlugChecker)
{
    private readonly SlugHelper _helper = new SlugHelper();
    public async Task<string> GenerateSlugFor(string vendorName)
    {


        var slug =  _helper.GenerateSlug(vendorName);

        if (await uniqueSlugChecker.CheckUniqueSlug(slug))
        {
            return slug;
        }

        var tries = Enumerable.Range(1, 20);
        foreach(var aTry in tries)
        {
            var candidate = $"{slug}-{aTry}";
            if(await uniqueSlugChecker.CheckUniqueSlug(candidate))
            {
                return candidate;
            }
        }

        return slug + "-" + Guid.NewGuid();
    }
}

public interface ICheckForUniqueVendorSlugs
{
    Task<bool> CheckUniqueSlug(string slug);
}

public class VendorDataService(IDocumentSession session) : ICheckForUniqueVendorSlugs, ICheckForVendorExistenceForCatalog
{
    public async Task<bool> CheckUniqueSlug(string slug)
    {
       return !await session.Query<VendorEntity>().AnyAsync( v => v.Slug == slug);

    }

    async Task<bool> ICheckForVendorExistenceForCatalog.DoesVendorExistAsync(string slug)
    {
        return !await this.CheckUniqueSlug(slug);
    }
}