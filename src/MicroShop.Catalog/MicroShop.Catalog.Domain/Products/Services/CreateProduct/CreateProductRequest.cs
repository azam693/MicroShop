using CommunityToolkit.Diagnostics;

namespace MicroShop.Catalog.Domain.Products.Services.CreateProduct;

public sealed class CreateProductRequest
{
    public string Sku { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string CategoryId { get; private set; }
    public string? Description { get; private set; }
    public string? Attribute { get; private set; }

    public CreateProductRequest(
        string sku,
        string name, 
        decimal price,
        string categoryId,
        string? description,
        string? attribute)
    {
        Guard.IsNotNullOrWhiteSpace(sku);
        Guard.IsNotNullOrWhiteSpace(name);
        Guard.IsGreaterThanOrEqualTo(price, 0);
        Guard.IsNotNullOrWhiteSpace(categoryId);

        Sku = sku;
        Name = name;
        Price = price;
        CategoryId = categoryId;
        Description = description;
        Attribute = attribute;
    }
}
