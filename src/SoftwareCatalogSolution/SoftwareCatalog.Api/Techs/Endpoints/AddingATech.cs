using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Techs.Endpoints;

public class AddingATech
{
    public static async Task<Results<Created<TechDetailsResponseModel>, BadRequest>> CanAddTechAsync(
        [FromBody] TechCreateModel request,
        [FromServices] IValidator<TechCreateModel> validator,
        [FromServices] IDocumentSession session,
        [FromServices] TechSlugGenerator slugGenerator,
        [FromServices] IHttpContextAccessor _httpContextAccessor
        )
    {
        var sub = _httpContextAccessor.HttpContext.User.Identity.Name;

        var validations = await validator.ValidateAsync(request);
        if (!validations.IsValid)
        {
            return TypedResults.BadRequest();
        }
        var entity = new TechEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Slug = await slugGenerator.GenerateSlugFor(request.Name),
            Contact = request.Contact,
            CreatedOn = DateTimeOffset.UnixEpoch
        };
        session.Store(entity);
        await session.SaveChangesAsync();
        var response = entity.MapToModel();
        return TypedResults.Created($"/techs/{entity.Slug}", response);
    }
}
