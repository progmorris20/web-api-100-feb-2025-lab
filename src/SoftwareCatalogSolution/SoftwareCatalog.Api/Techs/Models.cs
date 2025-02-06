using FluentValidation;

namespace SoftwareCatalog.Api.Techs;

public record TechCreateModel
{
    public string Name { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
}

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(2).MaximumLength(100);
        //some validation for contact info here, not looking up regex for email rn
    }
}
public record TechDetailsResponseModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public DateTimeOffset CreatedOn { get; set; }

}

