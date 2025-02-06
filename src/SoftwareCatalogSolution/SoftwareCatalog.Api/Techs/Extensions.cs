using FluentValidation;
using SoftwareCatalog.Api.Techs.Endpoints;

namespace SoftwareCatalog.Api.Techs;

public static class Extensions
{
    public static IServiceCollection AddTechs(this IServiceCollection services)
    {
        services.AddScoped<TechSlugGenerator>();
        services.AddScoped<ICheckForUniqueTechSlugs, TechDataService>();

        services.AddScoped<IValidator<TechCreateModel>, TechCreateModelValidator>();

        services.AddAuthorizationBuilder()
            .AddPolicy("canAddTechs", p =>
            {
                p.RequireRole("manager");
                p.RequireRole("software-center");
            });

        return services;
    }

    public static IEndpointRouteBuilder MapTechs(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("techs").WithTags("Technicians").WithDescription("The Company Technicians");

        group.MapPost("/", AddingATech.CanAddTechAsync).RequireAuthorization("canAddTechs");
        group.MapGet("/{id}", GettingATech.GetTechAsync).WithTags("Technicians");
        group.MapGet("/", GettingATech.GetTechsAsync);
        return group;

    }
}
