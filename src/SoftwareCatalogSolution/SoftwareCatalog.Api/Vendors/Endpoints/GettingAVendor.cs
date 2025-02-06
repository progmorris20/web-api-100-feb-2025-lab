
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Vendors.Endpoints;

public static class GettingAVendor
{

  
    public static  async Task<Results<Ok<VendorDetailsResponseModel>, NotFound>> GetVendorAsync(string id, IDocumentSession session)
    {
        var response = await session.Query<VendorEntity>()
            .Where(v => v.Slug == id)
            .ProjectToModel()
            .SingleOrDefaultAsync();

        return response switch
        {
            null => TypedResults.NotFound(),
            _ => TypedResults.Ok(response)
        };
    }

    public static async Task<Ok<IReadOnlyList<VendorDetailsResponseModel>>> GetVendorsAsync(IDocumentSession session)
    {
        var response = await session.Query<VendorEntity>().ProjectToModel().ToListAsync();
        return TypedResults.Ok(response);
    }

}