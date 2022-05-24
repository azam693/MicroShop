using Dawn;
using MediatR;

namespace Catalog.Application.Services.Products.CreateProduct;

public record CreateProductCommand : IRequest<Guid>
{
    public CreateProductCommand(
        string name,
        string description = null)
    {
        Name = Guard.Argument(name, nameof(name)).NotWhiteSpace();
        Description = description;
    }

    public string Name { get; }
    public string Description { get; }
}
