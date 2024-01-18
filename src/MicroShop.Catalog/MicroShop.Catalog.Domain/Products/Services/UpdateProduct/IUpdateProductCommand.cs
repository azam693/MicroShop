namespace MicroShop.Catalog.Domain.Products.Services.UpdateProduct;

public interface IUpdateProductCommand
{
    Task Handle(UpdateProductRequest request, CancellationToken cancellationToken);
}
