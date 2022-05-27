using Basket.Application.Services.ShoppingCarts.DeleteShoppingCartItem;
using Basket.Domain.ShoppringCarts;
using Basket.Infrastructure.EventStore.Repository;
using Dawn;
using Kernel.Contexts;
using MediatR;

namespace Basket.Application.Services.ShoppingCarts.ClearShoppingCartItem;

public class ClearShoppingCartItemHandler : IRequestHandler<ClearShoppingCartItemCommand, bool>
{
    private readonly IEventStoreRepository<ShoppingCart> _shoppingCartEventRepository;
    private readonly IOperationContext _operationContext;
    
    public ClearShoppingCartItemHandler(
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
    
    public async Task<bool> Handle(ClearShoppingCartItemCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();
        
        var shoppingCart = await _shoppingCartEventRepository.Get(
            request.ShoppingCartId,
            cancellationToken);
        shoppingCart.Clear(_operationContext);
        
        await _shoppingCartEventRepository.Delete(shoppingCart, cancellationToken);
        
        return true;
    }
}
