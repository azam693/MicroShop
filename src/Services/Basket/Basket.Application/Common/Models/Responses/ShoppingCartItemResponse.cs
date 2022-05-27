using Basket.Domain.ShoppringCarts;
using Dawn;

namespace Basket.Application.Common.Models.Responses;

public record ShoppingCartItemResponse
{
    public Guid ProductCombinationId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public ShoppingCartItemResponse(ShoppingCartItem shoppingCartItem)
    {
        Guard.Argument(shoppingCartItem, nameof(shoppingCartItem)).NotNull();

        ProductCombinationId = shoppingCartItem.ProductCombinationId;
        Quantity = shoppingCartItem.Quantity;
        Price = shoppingCartItem.Price;
    }
}
