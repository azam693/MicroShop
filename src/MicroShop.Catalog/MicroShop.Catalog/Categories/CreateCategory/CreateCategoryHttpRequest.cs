using MicroShop.Catalog.Domain.Categories.Services.CreateCategory;
using System.ComponentModel.DataAnnotations;

namespace MicroShop.Catalog.Web.Categories.CreateCategory;

public sealed record CreateCategoryHttpRequest([Required] string Name, string? ParentId)
{
    public CreateCategoryRequest CreateServiceRequest()
    {
        return new CreateCategoryRequest(Name, ParentId);
    }
}
