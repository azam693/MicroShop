using MicroShop.Catalog.Domain.Products.Entities;
using MicroShop.Catalog.Domain.Products.Services.CreateProduct;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;

namespace MicroShop.Catalog.Web.Products.CreateProduct;

public sealed class CreateProductEndpoint : IEndpoint<IResult, CreateProductHttpRequest, CancellationToken>
{
    private readonly ICreateProductCommand _createProductCommand;

    public CreateProductEndpoint(ICreateProductCommand createProductCommand)
    {
        _createProductCommand = createProductCommand;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product",
            ([FromBody] CreateProductHttpRequest request, CancellationToken cancellationToken)
                => HandleAsync(request, cancellationToken))
            .Produces(200)
            .WithTags(nameof(Product));
    }

    public async Task<IResult> HandleAsync(CreateProductHttpRequest request, CancellationToken cancellationToken)
    {
        var productId = await _createProductCommand.Handle(
            request.CreateServiceRequest(), 
            cancellationToken);

        return Results.Created($"api/product/{productId}", null);
    }
}
