using Basket.Domain.Common.Exceptions;
using Basket.Domain.ShoppringCarts;
using Basket.Infrastructure.EventStore.Repository;
using Dawn;
using Kernel.Contexts;
using MediatR;

namespace Basket.Application.Services.ShoppingCarts.AddShoppingCartItem;

public class AddShoppingCartItemHandler : IRequestHandler<AddShoppingCartItemCommand, Guid>
{
    private readonly IEventStoreRepository<ShoppingCart> _shoppingCartEventRepository;
    private readonly IOperationContext _operationContext;
    
    public AddShoppingCartItemHandler(
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
    
    public async Task<Guid> Handle(
        AddShoppingCartItemCommand command, 
        CancellationToken cancellationToken)
    {
        Guard.Argument(command, nameof(command)).NotNull();
        
        return command.ShoppingCartId.HasValue
            ? await UpdatehoppingCartItem(command, cancellationToken)
            : await CreateShoppingCart(command, cancellationToken);
    }

    private async Task<Guid> CreateShoppingCart(
        AddShoppingCartItemCommand command,
        CancellationToken cancellationToken)
    {
        var shoppingCart =  new ShoppingCart(
            shoppingCartItem: CreateShoppingCartItem(command),
            operationContext: _operationContext);
        
        await _shoppingCartEventRepository.Add(shoppingCart, cancellationToken);

        return shoppingCart.Id;
    }

    private async Task<Guid> UpdatehoppingCartItem(
        AddShoppingCartItemCommand command,
        CancellationToken cancellationToken)
    {
        var shoppingCartId = command.ShoppingCartId.Value;
        var shoppingCart = await _shoppingCartEventRepository.Get(
            shoppingCartId,
            cancellationToken)
            ?? throw new BasketException(
                $"Can't find shopping cart with id {shoppingCartId}");

        var shoppingCartItem = CreateShoppingCartItem(command);
        shoppingCart.AddItem(shoppingCartItem, _operationContext);

        await _shoppingCartEventRepository.Update(shoppingCart, cancellationToken);

        return shoppingCart.Id;
    }
    
    private ShoppingCartItem CreateShoppingCartItem(AddShoppingCartItemCommand command)
    {
        return new ShoppingCartItem(
            productCombinationId: command.ProductCombinationId,
            quantity: command.Quantity,
            price: 0);
    }
}
