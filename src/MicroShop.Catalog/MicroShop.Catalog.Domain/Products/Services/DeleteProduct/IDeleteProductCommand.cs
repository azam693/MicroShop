namespace MicroShop.Catalog.Domain.Products.Services.DeleteProduct;

public interface IDeleteProductCommand
{
    Task Handle(DeleteProductRequest request, CancellationToken cancellationToken);
}
