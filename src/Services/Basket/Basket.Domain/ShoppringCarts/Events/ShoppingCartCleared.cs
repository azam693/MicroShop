using Kernel.Contexts;

namespace Basket.Domain.ShoppringCarts.Events;

public sealed class ShoppingCartCleared : DomainEvent
{
    public ShoppingCartCleared(IOperationContext operationContext)
        : base(DateTime.UtcNow, operationContext)
    {
        
    }
}
