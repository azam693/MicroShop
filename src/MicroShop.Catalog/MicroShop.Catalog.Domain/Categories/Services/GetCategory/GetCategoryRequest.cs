using CommunityToolkit.Diagnostics;

namespace MicroShop.Catalog.Domain.Categories.Services.GetCategory;

public sealed class GetCategoryRequest
{
    public string Id { get; private set; }

    public GetCategoryRequest(string id)
    {
        Guard.IsNotNull(id);

        Id = id;
    }
}
