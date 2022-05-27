using Basket.Domain.ShoppringCarts;
using Basket.Infrastructure.EventStore.Repository;
using Dawn;
using Kernel.Contexts;
using MediatR;

namespace Basket.Application.Services.ShoppingCarts.UpdateProductInShoppingCartItem;

public class UpdateProductInShoppingCartItemHandler 
    : IRequestHandler<UpdateProductInShoppingCartItemCommand, bool>
{
    private readonly IEventStoreRepository<ShoppingCart> _shoppingCartEventRepository;
    private readonly IOperationContext _operationContext;
    
    public UpdateProductInShoppingCartItemHandler(
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
    
    public async Task<bool> Handle(UpdateProductInShoppingCartItemCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();
        
        var shoppingCart = await _shoppingCartEventRepository.Get(
            request.ShoppingCartId,
            cancellationToken);
        shoppingCart.UpdateProductItem(new ShoppingCartItem(
            productCombinationId: request.ProductCombinationId,
            quantity: request.Quantity,
            price: 0), 
            _operationContext);
        
        await _shoppingCartEventRepository.Update(shoppingCart, cancellationToken);
        
        return true;
    }
}
