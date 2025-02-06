using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Catalog.Endpoints;

[Tags("Catalog")]
public class GettingCatalogItemDetails : ControllerBase
{
    [HttpGet("/catalog/{id:guid}")]
    public async Task<ActionResult> GetItemById(Guid id, [FromServices] IDocumentSession session)
    {
        var item = await session.Query<CatalogItemEntity>()
            .Where(c => c.Id == id)
            .ProjectToDetailsModel()
            .SingleOrDefaultAsync();


        return item switch
        {
            null => NotFound(),
            _ => Ok(item),
        };
    }

    [HttpGet("/catalog")]
    public async Task<ActionResult> GetCatalogAsync(
        [FromServices] IDocumentSession session,
        [FromQuery] string? vendor = null)
    {
        var query = session.Query<CatalogItemEntity>();

        if (vendor != null)
        {
            query.Where(v => v.Vendor == vendor);
        }
        return Ok(await query.ProjectToDetailsModel().ToListAsync());
    }
}
