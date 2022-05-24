using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models.Responses;
using Dawn;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services.Products.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly ICatalogContext _catalogContext;
    
    public GetProductByIdHandler(
        ICatalogContext catalogContext)
    {
        Guard.Argument(catalogContext, nameof(catalogContext)).NotNull();
        
        _catalogContext = catalogContext;
    }
    
    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var product = await _catalogContext.Products.FirstOrDefaultAsync(
            product => product.Id == request.Id,
            cancellationToken);

        return new ProductResponse(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description);
    }
}
