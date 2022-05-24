using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models.Responses;
using Dawn;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services.Products.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductResponse>>
{
    private readonly ICatalogContext _catalogContext;
    
    public GetProductsHandler(
        ICatalogContext catalogContext)
    {
        Guard.Argument(catalogContext, nameof(catalogContext)).NotNull();
        
        _catalogContext = catalogContext;
    }
    
    public async Task<IEnumerable<ProductResponse>> Handle(
        GetProductsQuery request, 
        CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var productQuery = _catalogContext.Products
            .Skip(request.PageNumber * request.PageSize)
            .Take(request.PageSize);
        var products = await productQuery.ToListAsync(cancellationToken);

        return products.Select(
            product => new ProductResponse(
                Id: product.Id,
                Name: product.Name,
                Description: product.Description));
    }
}
