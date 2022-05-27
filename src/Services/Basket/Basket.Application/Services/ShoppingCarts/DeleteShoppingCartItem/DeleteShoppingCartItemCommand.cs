using Dawn;
using MediatR;

namespace Basket.Application.Services.ShoppingCarts.DeleteShoppingCartItem;

public class DeleteShoppingCartItemCommand : IRequest<bool>
{
    public DeleteShoppingCartItemCommand(
        Guid shoppingCartId,
        Guid productCombinationId)
    {
        ShoppingCartId = Guard.Argument(shoppingCartId, nameof(shoppingCartId)).NotDefault();
        ProductCombinationId = Guard
            .Argument(productCombinationId, nameof(productCombinationId))
            .NotDefault();
    }

    public Guid ShoppingCartId { get; protected set; }
    public Guid ProductCombinationId { get; protected set; }
}
