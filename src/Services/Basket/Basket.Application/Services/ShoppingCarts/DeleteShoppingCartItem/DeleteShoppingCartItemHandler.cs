using Basket.Domain.ShoppringCarts;
using Basket.Infrastructure.EventStore.Repository;
using Dawn;
using Kernel.Contexts;
using MediatR;

namespace Basket.Application.Services.ShoppingCarts.DeleteShoppingCartItem;

public class DeleteShoppingCartItemHandler : IRequestHandler<DeleteShoppingCartItemCommand, bool>
{
    private readonly IEventStoreRepository<ShoppingCart> _shoppingCartEventRepository;
    private readonly IOperationContext _operationContext;
    
    public DeleteShoppingCartItemHandler(
        IEventStoreRepository<ShoppingCart> shoppingCartEventRepository,
        IOperationContext operationContext)
    {
        _shoppingCartEventRepository = Guard
            .Argument(shoppingCartEventRepository, nameof(shoppingCartEventRepository))
            .NotNull()
            .Value;
        _operationContext = Guard
            .Argument(operationContext, nameof(operationContext))
            .NotNull()
            .Value;
    }
    
    public async Task<bool> Handle(
        DeleteShoppingCartItemCommand request, 
        CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();
        
        var shoppingCart = await _shoppingCartEventRepository.Get(
            request.ShoppingCartId,
            cancellationToken);
        shoppingCart.DeleteItem(
            productCombinationId: request.ProductCombinationId,
            operationContext: _operationContext);
        
        await _shoppingCartEventRepository.Delete(shoppingCart, cancellationToken);
        
        return true;
    }
}
