using Dawn;
using MediatR;

namespace Basket.Application.Services.ShoppingCarts.UpdateProductInShoppingCartItem;

public class UpdateProductInShoppingCartItemCommand : IRequest<bool>
{
    public UpdateProductInShoppingCartItemCommand(
        Guid shoppingCartId,
        Guid productCombinationId, 
        int quantity)
    {
        ShoppingCartId = Guard
            .Argument(shoppingCartId, nameof(shoppingCartId))
            .NotDefault();
        ProductCombinationId = Guard
            .Argument(productCombinationId, nameof(productCombinationId))
            .NotDefault();
        Quantity = Guard.Argument(quantity, nameof(quantity)).GreaterThan(0);
    }

    public Guid ShoppingCartId { get; protected set; }
    public Guid ProductCombinationId { get; protected set; }
    public int Quantity { get; protected set; }
}
