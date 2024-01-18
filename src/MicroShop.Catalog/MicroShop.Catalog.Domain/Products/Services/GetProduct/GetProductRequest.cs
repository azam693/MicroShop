using CommunityToolkit.Diagnostics;

namespace MicroShop.Catalog.Domain.Products.Services.GetProduct;

public sealed class GetProductRequest
{
    public string Id { get; private set; }
    public string? CategoryId { get; private set; }

    public GetProductRequest(string id, string? categoryId)
    {
        Guard.IsNotNullOrWhiteSpace(id);

        Id = id;
        CategoryId = categoryId;
    }
}
