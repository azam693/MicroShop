using MicroShop.Catalog.Domain.Categories.Entities;
using MicroShop.Catalog.Domain.Categories.Services.CreateCategory;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;

namespace MicroShop.Catalog.Web.Categories.CreateCategory;

public class CreateCategoryEndpoint : IEndpoint<IResult, CreateCategoryHttpRequest, CancellationToken>
{
    private readonly ICreateCategoryCommand _createCategoryCommand;

    public CreateCategoryEndpoint(ICreateCategoryCommand createCategoryCommand)
    {
        _createCategoryCommand = createCategoryCommand;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/category",
            ([FromBody] CreateCategoryHttpRequest request, CancellationToken cancellationToken)
                => HandleAsync(request, cancellationToken))
            .Produces(200)
            .WithTags(nameof(Category));
    }

    public async Task<IResult> HandleAsync(CreateCategoryHttpRequest request, CancellationToken cancellationToken)
    {
        var categoryId = await _createCategoryCommand.Handle(request.CreateServiceRequest(), cancellationToken);

        return Results.Created($"api/category/{categoryId}", null);
    }
}
