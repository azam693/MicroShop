namespace MicroShop.ShoppingCart.Domain.Common;

public sealed class ShoppingCartException : Exception
{
    public ShoppingCartException(string message)
        : base(message)
    {
        
    }
}
