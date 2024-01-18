namespace MicroShop.Catalog.Domain.Products.Services.UpdateProduct;

internal sealed class UpdateProductCommand : IUpdateProductCommand
{
    public UpdateProductCommand()
    {
        
    }

    public Task Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
