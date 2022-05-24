using Dawn;
using Kernel.Entities;

namespace Catalog.Domain.Entities.Products;

public class ProductCombination : GuidEntity
{
    private ProductCombination() { }

    public ProductCombination(
        string combinationOptionIds,
        decimal price,
        Product product)
    {
        CombinationOptionIds = Guard
            .Argument(combinationOptionIds, nameof(combinationOptionIds))
            .NotWhiteSpace();
        Price = Guard.Argument(price, nameof(price)).GreaterThan(-1);
        Product = Guard.Argument(product, nameof(product)).NotNull();
    }

    public string CombinationOptionIds { get; protected set; }
    public decimal Price { get; protected set; }

    public Product Product { get; protected set; }
}
