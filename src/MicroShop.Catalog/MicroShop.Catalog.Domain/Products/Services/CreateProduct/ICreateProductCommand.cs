namespace MicroShop.Catalog.Domain.Products.Services.CreateProduct;

public interface ICreateProductCommand
{
    Task<string> Handle(CreateProductRequest request, CancellationToken cancellationToken);
}
