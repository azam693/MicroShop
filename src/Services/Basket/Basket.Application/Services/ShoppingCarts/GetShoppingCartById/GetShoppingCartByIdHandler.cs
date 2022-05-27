using Basket.Application.Common.Models.Responses;
using Basket.Domain.ShoppringCarts;
using Basket.Infrastructure.EventStore.Repository;
using Dawn;
using Kernel.Contexts;
using MediatR;

namespace Basket.Application.Services.ShoppingCarts.GetShoppingCartById;

public class GetShoppingCartByIdHandler 
    : IRequestHandler<GetShoppingCartByIdQuery, ShoppingCartResponse>
{
    private readonly IEventStoreRepository<ShoppingCart> _shoppingCartEventRepository;
    private readonly IOperationContext _operationContext;
    
    public GetShoppingCartByIdHandler(
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
    
    public async Task<ShoppingCartResponse> Handle(
        GetShoppingCartByIdQuery request, 
        CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var shoppingCart = await _shoppingCartEventRepository
            .Get(request.Id, cancellationToken);

        return new ShoppingCartResponse(shoppingCart);
    }
}
