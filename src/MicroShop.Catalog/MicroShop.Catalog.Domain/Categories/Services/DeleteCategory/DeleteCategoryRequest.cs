using CommunityToolkit.Diagnostics;

namespace MicroShop.Catalog.Domain.Categories.Services.DeleteCategory;

public sealed class DeleteCategoryRequest
{
    public string Id { get; private set; }

    public DeleteCategoryRequest(string id)
    {
        Guard.IsNotNull(id);

        Id = id;
    }
}
