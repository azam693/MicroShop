namespace Catalog.Domain.Exceptions;

public class CatalogException : Exception
{
    public CatalogException(string message, Exception? exception = null)
        : base(message, exception)
    {
        
    }
}