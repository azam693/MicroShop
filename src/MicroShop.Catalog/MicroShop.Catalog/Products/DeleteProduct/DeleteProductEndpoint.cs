using MicroShop.Catalog.Domain.Products.Entities;
using MicroShop.Catalog.Domain.Products.Services.DeleteProduct;
using MinimalApi.Endpoint;

namespace MicroShop.Catalog.Web.Products.DeleteProduct;

public sealed class DeleteProductEndpoint : IEndpoint<IResult, DeleteProductHttpRequest, CancellationToken>
{
    private readonly IDeleteProductCommand _deleteProductCommand;

    public DeleteProductEndpoint(IDeleteProductCommand deleteProductCommand)
    {
        _deleteProductCommand = deleteProductCommand;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/product/{id}",
            ([AsParameters] DeleteProductHttpRequest request, CancellationToken cancellationToken)
                => HandleAsync(request, cancellationToken))
            .Produces(200)
            .WithTags(nameof(Product));
    }

    public async Task<IResult> HandleAsync(DeleteProductHttpRequest request, CancellationToken cancellationToken)
    {
        await _deleteProductCommand.Handle(request.CreateServiceRequest(), cancellationToken);

        return Results.Ok();
    }
}
