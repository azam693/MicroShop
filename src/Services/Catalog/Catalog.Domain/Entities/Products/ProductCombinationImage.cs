using Dawn;
using Kernel.Entities;

namespace Catalog.Domain.Entities.Products;

public class ProductCombinationImage : GuidEntity
{
    private ProductCombinationImage() { }

    public ProductCombinationImage(
        ProductCombination productCombination,
        string path)
        : base(Guid.NewGuid())
    {
        ProductCombination = Guard.Argument(productCombination, nameof(productCombination)).NotNull();
        Path = Guard.Argument(path).NotWhiteSpace();
    }
 
    public ProductCombination ProductCombination { get; protected set; }
    public string Path { get; protected set; }
}
