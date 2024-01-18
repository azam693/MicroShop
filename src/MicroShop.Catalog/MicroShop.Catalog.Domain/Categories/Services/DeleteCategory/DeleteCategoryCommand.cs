using CommunityToolkit.Diagnostics;
using MicroShop.Catalog.Domain.Categories.Entities;
using MicroShop.Catalog.Domain.Common;

namespace MicroShop.Catalog.Domain.Categories.Services.DeleteCategory;

public sealed class DeleteCategoryCommand : IDeleteCategoryCommand
{
    private readonly IRepository<Category> _categoryRepository;

    public DeleteCategoryCommand(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(request);

        await _categoryRepository.DeleteAsync(
            request.Id, 
            Category.MainPartitionKey, 
            cancellationToken);
    }
}
