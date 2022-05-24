using Dawn;
using MediatR;

namespace Catalog.Application.Services.Categories.UpdateCategory;

public record UpdateCategoryCommand : IRequest<bool>
{
    public UpdateCategoryCommand(
        Guid id,
        string name,
        Guid? parentId = null)
    {
        Id = Guard.Argument(id, nameof(id)).NotDefault();
        Name = Guard.Argument(name, nameof(name)).NotWhiteSpace();
        ParentId = parentId;
    }

    public Guid Id { get; }
    public string Name { get; }
    public Guid? ParentId { get; }
}
