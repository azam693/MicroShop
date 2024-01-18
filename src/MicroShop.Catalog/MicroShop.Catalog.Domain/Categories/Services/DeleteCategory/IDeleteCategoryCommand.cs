namespace MicroShop.Catalog.Domain.Categories.Services.DeleteCategory;

public interface IDeleteCategoryCommand
{
    Task Handle(DeleteCategoryRequest request, CancellationToken cancellationToken);
}
