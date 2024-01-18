namespace MicroShop.Catalog.Domain.Categories.Services.GetCategory;

public interface IGetCategoryQuery
{
    Task<GetCategoryResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken);
}
