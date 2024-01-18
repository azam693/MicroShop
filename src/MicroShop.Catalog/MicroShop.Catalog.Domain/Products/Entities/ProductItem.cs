using CommunityToolkit.Diagnostics;

namespace MicroShop.Catalog.Domain.Products.Entities;

public sealed class ProductItem
{
    public string Sku { get; private set; }
    public decimal Price { get; private set; }
    public string? Attribute { get; private set; }

    private ProductItem() { }

    public ProductItem(
        string sku,
        decimal price,
        string? attribute)
    {
        Guard.IsNotNullOrWhiteSpace(sku);
        Guard.IsGreaterThanOrEqualTo(price, 0);
        
        Sku = sku;
        Price = price;
        Attribute = attribute;
    }
}
