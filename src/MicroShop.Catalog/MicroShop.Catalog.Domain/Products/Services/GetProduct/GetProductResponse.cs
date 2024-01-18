using MicroShop.Catalog.Domain.Products.Entities;

namespace MicroShop.Catalog.Domain.Products.Services.GetProduct;

public sealed record GetProductResponse(
    string Name,
    string? Description,
    DateTime CreateDate,
    IReadOnlyCollection<ProductItem> Items)
{
    public static GetProductResponse Create(Product product)
    {
        return new GetProductResponse(
            product.Name,
            product.Description,
            product.CreateDate,
            product.Items);
    }
}
