using Dawn;
using MediatR;

namespace Basket.Application.Services.ShoppingCarts.AddShoppingCartItem;

public class AddShoppingCartItemCommand : IRequest<Guid>
{
    public AddShoppingCartItemCommand(
        Guid productCombinationId, 
        int quantity,
        Guid? shoppingCartId = null)
    {
        ProductCombinationId = Guard
            .Argument(productCombinationId, nameof(productCombinationId))
            .NotDefault();
        Quantity = Guard.Argument(quantity, nameof(quantity)).GreaterThan(0);
        ShoppingCartId = shoppingCartId;
    }

    public Guid ProductCombinationId { get; protected set; }
    public int Quantity { get; protected set; }
    public Guid? ShoppingCartId { get; protected set; }
}
