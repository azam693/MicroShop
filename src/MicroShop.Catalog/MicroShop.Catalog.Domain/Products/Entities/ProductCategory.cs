using CommunityToolkit.Diagnostics;

namespace MicroShop.Catalog.Domain.Products.Entities;

public sealed class ProductCategory
{
    public string Id { get; private set; }
    public string Name { get; private set; }

    public ProductCategory(string id, string name)
    {
        Guard.IsNotNull(id);
        Guard.IsNotNull(name);

        Id = id;
        Name = name;
    }
}
