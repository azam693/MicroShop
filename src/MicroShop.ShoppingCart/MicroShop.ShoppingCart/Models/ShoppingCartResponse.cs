using MicroShop.ShoppingCart.Domain;

namespace MicroShop.ShoppingCart.Web.Models;

public sealed record ShoppingCartResponse(
    IReadOnlyCollection<ShoppingCartItem> Items,
    decimal TotalPrice)
{
    public static ShoppingCartResponse Create(Domain.ShoppingCart shoppingCart)
    {
        return new ShoppingCartResponse(
            shoppingCart.Items,
            shoppingCart.CalculateTotalPrice());
    }
}
