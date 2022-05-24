using Catalog.Application.Common.Models.Responses;
using Dawn;
using MediatR;

namespace Catalog.Application.Services.Products.GetProductById;

public record GetProductByIdQuery : IRequest<ProductResponse>
{
    public GetProductByIdQuery(Guid id)
    {
        Id = Guard.Argument(id, nameof(id)).NotDefault();
    }

    public Guid Id { get; }
}
