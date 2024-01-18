using MicroShop.Catalog.Domain.Products.Services.DeleteProduct;
using System.ComponentModel.DataAnnotations;

namespace MicroShop.Catalog.Web.Products.DeleteProduct;

public sealed record DeleteProductHttpRequest([Required] string Id, [Required] string CategoryName)
{
    public DeleteProductRequest CreateServiceRequest()
    {
        return new DeleteProductRequest(Id, CategoryName);
    }
}
