namespace MicroShop.Catalog.Domain.Common;

public sealed class CatalogException : Exception
{
    public CatalogException(string message)
        : base(message)
    {
        
    }
}
