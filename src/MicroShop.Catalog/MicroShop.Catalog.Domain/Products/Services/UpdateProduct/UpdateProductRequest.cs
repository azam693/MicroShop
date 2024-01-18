using CommunityToolkit.Diagnostics;

namespace MicroShop.Catalog.Domain.Products.Services.UpdateProduct;

public sealed class UpdateProductRequest
{
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public UpdateProductRequest(string name, string? description)
    {
        Guard.IsNotNullOrWhiteSpace(name);

        Name = name;
        Description = description;
    }
}
