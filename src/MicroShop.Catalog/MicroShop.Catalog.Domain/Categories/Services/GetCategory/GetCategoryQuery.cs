using MicroShop.Catalog.Domain.Categories.Entities;
using MicroShop.Catalog.Domain.Common;

namespace MicroShop.Catalog.Domain.Categories.Services.GetCategory;

public sealed class GetCategoryQuery : IGetCategoryQuery
{
    private readonly IRepository<Category> _categoryRepository;

    public GetCategoryQuery(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<GetCategoryResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAsync(
            request.Id, 
            Category.MainPartitionKey, 
            cancellationToken);

        return GetCategoryResponse.Create(category);
    }
}
