using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models.Responses;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Dawn;
using Kernel.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services.Products.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly ICatalogContext _catalogContext;
    
    public DeleteProductHandler(
        ICatalogContext catalogContext)
    {
        Guard.Argument(catalogContext, nameof(catalogContext)).NotNull();
        
        _catalogContext = catalogContext;
    }
    
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var product = await _catalogContext.Products.FirstOrDefaultAsync(
                product => product.Id == request.Id,
                cancellationToken)
            ?? throw new CatalogException($"Product with Id {request.Id} doesn't exist");
        
        _catalogContext.Products.Remove(product);

        return await _catalogContext.SaveAsync(cancellationToken) > 0;
    }
}
