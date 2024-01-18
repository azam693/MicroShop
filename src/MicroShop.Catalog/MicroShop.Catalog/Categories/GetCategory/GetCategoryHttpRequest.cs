using MicroShop.Catalog.Domain.Categories.Services.GetCategory;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MicroShop.Catalog.Web.Categories.GetCategory;

public sealed record GetCategoryHttpRequest([FromRoute][Required] string Id)
{
    public GetCategoryRequest CreateServiceRequest()
    {
        return new GetCategoryRequest(Id);
    }
}
