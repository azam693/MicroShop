using MicroShop.ShoppingCart.Domain;
using System.ComponentModel.DataAnnotations;

namespace MicroShop.ShoppingCart.Web.Models;

public sealed record ShoppingCartItemHttpRequest(
    [Required] string ProductId,
    [Range(1, int.MaxValue)] int Quantity, 
    [Range(0, int.MaxValue)] decimal Price)
{
    public ShoppingCartItem CreateShoppingCartItem()
    {
        return new ShoppingCartItem(ProductId, Quantity, Price);
    }
}
