using Dawn;
using MediatR;

namespace Basket.Application.Services.ShoppingCarts.ClearShoppingCartItem;

public class ClearShoppingCartItemCommand : IRequest<bool>
{
    public ClearShoppingCartItemCommand(Guid shoppingCartId)
    {
        ShoppingCartId = Guard.Argument(shoppingCartId, nameof(shoppingCartId)).NotDefault();
    }

    public Guid ShoppingCartId { get; protected set; }
}
