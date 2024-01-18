using MicroShop.Catalog.Domain.Products.Entities;
using MicroShop.Catalog.Domain.Products.Services.GetProduct;
using MinimalApi.Endpoint;

namespace MicroShop.Catalog.Web.Products.GetProduct;

public sealed class GetProductEndpoint : IEndpoint<IResult, GetProductHttpRequest, CancellationToken>
{
    private readonly IGetProductQuery _productQuery;

    public GetProductEndpoint(IGetProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/product/{id}",
            ([AsParameters] GetProductHttpRequest request, CancellationToken cancellationToken)
                => HandleAsync(request, cancellationToken))
            .Produces<GetProductResponse>()
            .WithTags(nameof(Product));
    }

    public async Task<IResult> HandleAsync(GetProductHttpRequest request, CancellationToken cancellationToken)
    {
        var response = await _productQuery.Handle(request.CreateServiceRequest(), cancellationToken);

        return Results.Ok(response);
    }
}
