using Dawn;
using Kernel.Contexts;

namespace Basket.Domain.ShoppringCarts.Events;

public sealed class ShoppingCartInitialized : DomainEvent
{
    public ShoppingCartInitialized(
        Guid id,
        ShoppingCartItem shoppingCartItem,
        IOperationContext operationContext)
        : base(DateTime.UtcNow, operationContext)
    {
        Id = Guard.Argument(id, nameof(id)).NotDefault();
        ShoppingCartItem = Guard
            .Argument(shoppingCartItem, nameof(shoppingCartItem))
            .NotNull();
    }

    public Guid Id { get; }
    public ShoppingCartItem ShoppingCartItem { get; }
}
