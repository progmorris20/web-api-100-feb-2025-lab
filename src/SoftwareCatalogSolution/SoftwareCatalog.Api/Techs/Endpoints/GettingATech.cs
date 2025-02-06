
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SoftwareCatalog.Api.Techs.Endpoints;

public class GettingATech
{
    public static async Task<Results<Ok<TechDetailsResponseModel>, NotFound>> GetTechAsync(string id, IDocumentSession session)
    {
        var response = await session.Query<TechEntity>()
            .Where(v => v.Slug == id)
            .ProjectToModel()
            .SingleOrDefaultAsync();

        return response switch
        {
            null => TypedResults.NotFound(),
            _ => TypedResults.Ok(response)
        };
    }

    public static async Task<Ok<IReadOnlyList<TechDetailsResponseModel>>> GetTechsAsync(IDocumentSession session)
    {
        var response = await session.Query<TechEntity>().ProjectToModel().ToListAsync();
        return TypedResults.Ok(response);
    }
}
