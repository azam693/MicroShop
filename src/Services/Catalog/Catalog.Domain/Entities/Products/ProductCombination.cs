using Dawn;
using Kernel.Entities;

namespace Catalog.Domain.Entities.Products;

public class ProductCombination : GuidEntity
{
    private ProductCombination() { }

    public ProductCombination(
        string combinationOptionIds,
        string sku,
        decimal price,
        int quantity,
        Product product)
    {
        CombinationOptionIds = Guard
            .Argument(combinationOptionIds, nameof(combinationOptionIds))
            .NotWhiteSpace();
        Price = Guard.Argument(price, nameof(price)).GreaterThan(-1);
        Product = Guard.Argument(product, nameof(product)).NotNull();
        Quantity = Guard.Argument(quantity, nameof(quantity)).GreaterThan(-1);
        Sku = sku;
    }

    public string CombinationOptionIds { get; protected set; }
    public string Sku { get; protected set; }
    public decimal Price { get; protected set; }
    public int Quantity { get; protected set; }

    public Product Product { get; protected set; }
}
