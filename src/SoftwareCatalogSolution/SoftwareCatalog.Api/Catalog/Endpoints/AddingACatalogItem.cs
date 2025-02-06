using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareCatalog.Api.Shared.Catalog;
using SoftwareCatalog.Api.Vendors;

namespace SoftwareCatalog.Api.Catalog.Endpoints;
[Tags("Catalog")]
[Produces("application/json")]
[ProducesResponseType(201)]
[ProducesResponseType(400)]
public class AddingACatalogItem(IDocumentSession session, 
    IValidator<CatalogItemRequestModel> validator, 
    ICheckForVendorExistenceForCatalog catalogChecker) : ControllerBase
{
    // GET /catalog/pizza -> 404


    [HttpPost("/vendors/{vendor:alpha}/opensource")]
    public async Task<ActionResult<CatalogItemResponseDetailsModel>> AddOpenSourceItemToCatalogAsync(
        [FromRoute] string vendor,
        [FromBody] CatalogItemRequestModel request) => await AddCatalogItem(request, CatalogItemLicenceTypes.OpenSource, vendor);

    [HttpPost("/vendors/{vendor:alpha}/free")]
    public async Task<ActionResult> AddFreeItemToCatalogAsync(
        [FromRoute] string vendor,
       [FromBody] CatalogItemRequestModel request) => await AddCatalogItem(request, CatalogItemLicenceTypes.Free, vendor);


    [HttpPost("/vendors/{vendor:alpha}/paid")]
    public async Task<ActionResult> AddPaidSourceItemToCatalogAsync(
        [FromRoute] string vendor,
        [FromBody] CatalogItemRequestModel request) => await AddCatalogItem(request, CatalogItemLicenceTypes.Paid, vendor);

    private async Task<ActionResult> AddCatalogItem(CatalogItemRequestModel request, CatalogItemLicenceTypes license, string vendor)
    {
        // TODO: 1. Do Some validation
        // TODO : 2 save it to the database.

        if (await catalogChecker.DoesVendorExistAsync(vendor))
        {
            var validationResults = await validator.ValidateAsync(request);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToDictionary()); // 400
            }

            var entityToSave = request.ToCatalogItemEntity(vendor, license);

            session.Store(entityToSave);
            await session.SaveChangesAsync(); // Do the actual work and save into the database

            // Step 3: send them the response
            // fake this for right now
            var response = entityToSave.ToCatalogItemResponseModel();
            return StatusCode(201, response);
        } else
        {
            return NotFound("Vendor Does Not Exist");
        }
    }
}





