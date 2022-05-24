using Dawn;
using MediatR;

namespace Catalog.Application.Services.Categories.DeleteCategory;

public record DeleteCategoryCommand : IRequest<bool>
{
    public DeleteCategoryCommand(Guid id)
    {
        Id = Guard.Argument(id, nameof(id)).NotDefault();
    }

    public Guid Id { get; }
}
