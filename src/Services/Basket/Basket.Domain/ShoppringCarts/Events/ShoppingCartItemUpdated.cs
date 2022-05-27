using Dawn;
using Kernel.Contexts;

namespace Basket.Domain.ShoppringCarts.Events;

public sealed class ShoppingCartItemUpdated : DomainEvent
{
    public ShoppingCartItemUpdated(
        ShoppingCartItem shoppingCartItem,
        IOperationContext operationContext)
        : base(DateTime.UtcNow, operationContext)
    {
        ShoppingCartItem = Guard
            .Argument(shoppingCartItem, nameof(shoppingCartItem))
            .NotNull();
    }

    public ShoppingCartItem ShoppingCartItem { get; }
}
