namespace Basket.Domain.Common.Exceptions;

public class BasketException : Exception
{
    public BasketException(string message, Exception? ex = null)
        : base(message, ex)
    {
        
    }
}
