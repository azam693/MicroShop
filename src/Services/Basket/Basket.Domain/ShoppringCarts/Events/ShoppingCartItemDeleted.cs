using Dawn;
using Kernel.Contexts;

namespace Basket.Domain.ShoppringCarts.Events;

public sealed class ShoppingCartItemDeleted : DomainEvent
{
    public ShoppingCartItemDeleted(
        Guid productCombinationId,
        IOperationContext operationContext)
        : base(DateTime.UtcNow, operationContext)
    {
        ProductCombinationId = Guard
            .Argument(productCombinationId, nameof(productCombinationId))
            .NotDefault();
    }

    public Guid ProductCombinationId { get; }
}
