using Basket.Domain.ShoppringCarts;
using Dawn;

namespace Basket.Application.Common.Models.Responses;

public record ShoppingCartResponse
{
    public IReadOnlyCollection<ShoppingCartItemResponse> ShoppingCartItemResponses { get; protected set; }
    public decimal TotalPrice { get; protected set; }
    
    public ShoppingCartResponse(ShoppingCart shoppingCart)
    {
        Guard.Argument(shoppingCart, nameof(shoppingCart)).NotNull();

        ShoppingCartItemResponses = shoppingCart.ShoppingCartItems
            .Select(item => new ShoppingCartItemResponse(item))
            .ToArray();
        TotalPrice = shoppingCart.TotalPrice;
    }
}
