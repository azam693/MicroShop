using Basket.Application.Common.Models.Responses;
using Dawn;
using MediatR;

namespace Basket.Application.Services.ShoppingCarts.GetShoppingCartById;

public record GetShoppingCartByIdQuery : IRequest<ShoppingCartResponse>
{
    public Guid Id { get; protected set; }
    
    public GetShoppingCartByIdQuery(Guid id)
    {
        Id = Guard.Argument(id, nameof(id)).NotDefault();
    }
}
