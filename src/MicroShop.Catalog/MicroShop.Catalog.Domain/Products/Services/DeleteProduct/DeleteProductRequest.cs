using CommunityToolkit.Diagnostics;

namespace MicroShop.Catalog.Domain.Products.Services.DeleteProduct;

public sealed class DeleteProductRequest
{
    public string Id { get; private set; }
    public string CategoryName { get; private set; }

    public DeleteProductRequest(string id, string categoryName)
    {
        Guard.IsNotNullOrWhiteSpace(id);
        Guard.IsNotNullOrWhiteSpace(categoryName);

        Id = id;
        CategoryName = categoryName;
    }
}
