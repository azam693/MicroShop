namespace MicroShop.Catalog.Domain.Products.Services.GetProduct;

public interface IGetProductQuery
{
    Task<GetProductResponse> Handle(GetProductRequest request, CancellationToken cancellationToken);
}
