using Dawn;
using MediatR;

namespace Catalog.Application.Services.Products.UpdateProduct;

public record UpdateProductCommand : IRequest<bool>
{
    public UpdateProductCommand(
        Guid id,
        string name,
        string description = null)
    {
        Id = Guard.Argument(id, nameof(id)).NotDefault();
        Name = Guard.Argument(name, nameof(name)).NotWhiteSpace();
        Description = description;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
}
