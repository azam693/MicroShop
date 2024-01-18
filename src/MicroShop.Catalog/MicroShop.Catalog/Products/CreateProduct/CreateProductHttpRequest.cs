using MicroShop.Catalog.Domain.Products.Services.CreateProduct;
using System.ComponentModel.DataAnnotations;

namespace MicroShop.Catalog.Web.Products.CreateProduct;

public sealed record CreateProductHttpRequest(
    [Required] string Sku,
    [Required] string Name,
    [Range(0, int.MaxValue)] decimal Price,
    [Required] string CategoryId,
    string? Description,
    string? Attribute)
{
    public CreateProductRequest CreateServiceRequest()
    {
        return new CreateProductRequest(
            Sku,
            Name,
            Price,
            CategoryId,
            Description,
            Attribute);
    }
}
