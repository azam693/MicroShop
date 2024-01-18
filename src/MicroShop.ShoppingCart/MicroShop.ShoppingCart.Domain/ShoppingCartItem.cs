using CommunityToolkit.Diagnostics;

namespace MicroShop.ShoppingCart.Domain;

public sealed class ShoppingCartItem
{
    public string ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public ShoppingCartItem(string productId, int quantity, decimal price)
    {
        Guard.IsNotNull(productId);
        Guard.IsGreaterThanOrEqualTo(price, 0);

        ProductId = productId;
        Quantity = quantity > 0 ? quantity : 1;
        Price = price;
    }
}
