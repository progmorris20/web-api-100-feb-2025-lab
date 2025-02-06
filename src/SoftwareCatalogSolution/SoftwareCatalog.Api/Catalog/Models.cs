using FluentValidation;

namespace SoftwareCatalog.Api.Catalog;

public record CatalogItemRequestModel
{
    public string Name { get; set; } = string.Empty;

}

public class CatalogItemRequestModelValidator : AbstractValidator<CatalogItemRequestModel>
{
    public CatalogItemRequestModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MinimumLength(3);
        RuleFor(x => x.Name).MaximumLength(100);
    }
}


public record CatalogItemResponseDetailsModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Vendor { get; set; } = string.Empty;
    public CatalogItemLicenceTypes Licence { get; set; }

}