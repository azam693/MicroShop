namespace MicroShop.Catalog.Domain.Categories.Services.CreateCategory;

public interface ICreateCategoryCommand
{
    Task<string> Handle(CreateCategoryRequest request, CancellationToken cancellationToken);
}
