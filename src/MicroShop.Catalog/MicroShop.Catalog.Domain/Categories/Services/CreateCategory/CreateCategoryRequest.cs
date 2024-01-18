using CommunityToolkit.Diagnostics;

namespace MicroShop.Catalog.Domain.Categories.Services.CreateCategory;

public sealed class CreateCategoryRequest
{
    public string Name { get; private set; }
    public string? ParentId { get; private set; }

    public CreateCategoryRequest(string name, string? parentId)
    {
        Guard.IsNotNull(name);

        Name = name;
        ParentId = parentId;
    }
}
