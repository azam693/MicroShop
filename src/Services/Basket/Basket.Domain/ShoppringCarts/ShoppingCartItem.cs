using Dawn;

namespace Basket.Domain.ShoppringCarts;

public class ShoppingCartItem
{
    public ShoppingCartItem(
        Guid productCombinationId,
        int quantity,
        decimal price)
    {
        ProductCombinationId = Guard.Argument(productCombinationId, nameof(productCombinationId)).NotDefault();
        Quantity = Guard.Argument(quantity, nameof(quantity)).GreaterThan(0);
        Price = price;
    }

    public Guid ProductCombinationId { get; protected set; }
    public int Quantity { get; protected set; }
    public decimal Price { get; protected set; }
}