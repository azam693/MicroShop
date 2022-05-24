using Dawn;
using MediatR;

namespace Catalog.Application.Services.Categories.CreateCategory;

public record CreateCategoryCommand : IRequest<Guid>
{
    public CreateCategoryCommand(
        string name,
        Guid? parentId = null)
    {
        Name = Guard.Argument(name, nameof(name)).NotWhiteSpace();
        ParentId = parentId;
    }

    public string Name { get; }
    public Guid? ParentId { get; }
}
