using Dawn;
using MediatR;

namespace Catalog.Application.Services.Products.DeleteProduct;

public record DeleteProductCommand : IRequest<bool>
{
    public DeleteProductCommand(Guid id)
    {
        Id = Guard.Argument(id, nameof(id)).NotDefault();
    }

    public Guid Id { get; }
}
