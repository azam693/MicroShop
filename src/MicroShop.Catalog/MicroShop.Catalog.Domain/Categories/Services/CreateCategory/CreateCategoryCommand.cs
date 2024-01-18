using CommunityToolkit.Diagnostics;
using MicroShop.Catalog.Domain.Categories.Entities;
using MicroShop.Catalog.Domain.Common;

namespace MicroShop.Catalog.Domain.Categories.Services.CreateCategory;

public sealed class CreateCategoryCommand : ICreateCategoryCommand
{
    private readonly IRepository<Category> _categoryRepository;

    public CreateCategoryCommand(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<string> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(request);

        var category = new Category(request.Name, request.ParentId);

        await _categoryRepository.CreateAsync(
            category, 
            category.Type, 
            cancellationToken);

        return category.Id;
    }
}
