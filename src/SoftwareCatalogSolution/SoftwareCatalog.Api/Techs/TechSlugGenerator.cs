using Marten;
using Slugify;

namespace SoftwareCatalog.Api.Techs;

public interface ICheckForUniqueTechSlugs
{
    Task<bool> CheckUniqueSlug(string slug);
}

public class TechSlugGenerator(ICheckForUniqueTechSlugs uniqueSlugChecker)
{

    private readonly SlugHelper _helper = new SlugHelper();
    public async Task<string> GenerateSlugFor(string techName)
    {
        var slug = _helper.GenerateSlug(techName);

        if (await uniqueSlugChecker.CheckUniqueSlug(slug))
        {
            return slug;
        }

        var tries = Enumerable.Range(1, 20);
        foreach (var aTry in tries)
        {
            var candidate = $"{slug}-{aTry}";
            if (await uniqueSlugChecker.CheckUniqueSlug(candidate))
            {
                return candidate;
            }
        }

        return slug + "-" + Guid.NewGuid();
    }
}

public class TechDataService(IDocumentSession session) : ICheckForUniqueTechSlugs
{
    public async Task<bool> CheckUniqueSlug(string slug)
    {
        return !await session.Query<TechEntity>().AnyAsync(v => v.Slug == slug);

    }
}