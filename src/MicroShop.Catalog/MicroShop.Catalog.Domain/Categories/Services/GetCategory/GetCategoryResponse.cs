using MicroShop.Catalog.Domain.Categories.Entities;

namespace MicroShop.Catalog.Domain.Categories.Services.GetCategory;

public sealed record GetCategoryResponse(string Id, string Name, string? ParentId)
{
    public static GetCategoryResponse Create(Category category)
    {
        return new GetCategoryResponse(
            category.Id,
            category.Name,
            category.ParentId);
    }
}
