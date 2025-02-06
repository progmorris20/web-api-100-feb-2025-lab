namespace SoftwareCatalog.Api.Techs;

public class TechEntity
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public DateTimeOffset CreatedOn { get; set; }
}
