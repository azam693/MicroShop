using MicroShop.Catalog.Domain.Products.Services.GetProduct;
using System.ComponentModel.DataAnnotations;

namespace MicroShop.Catalog.Web.Products.GetProduct;

public sealed record GetProductHttpRequest([Required] string Id, string? CategoryId)
{
    public GetProductRequest CreateServiceRequest()
    {
        return new GetProductRequest(Id, CategoryId);
    }
}
