using Dawn;
using Kernel.Entities;

namespace Catalog.Domain.Entities.Products;

public class ProductImage : GuidEntity
{
    private ProductImage() { }

    public ProductImage(
        Product product,
        string path)
        : base(Guid.NewGuid())
    {
        Product = Guard.Argument(product, nameof(product)).NotNull();
        Path = Guard.Argument(path).NotWhiteSpace();
    }

    public Product Product { get; protected set; }
    public string Path { get; protected set; }
}
