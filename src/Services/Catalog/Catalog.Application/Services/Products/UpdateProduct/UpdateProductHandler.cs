using Catalog.Application.Common.Interfaces;
using Catalog.Domain.Exceptions;
using Dawn;
using Kernel.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services.Products.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly ICatalogContext _catalogContext;
    private readonly IOperationContext _operationContext;
    
    public UpdateProductHandler(
        ICatalogContext catalogContext,
        IOperationContext operationContext)
    {
        Guard.Argument(catalogContext, nameof(catalogContext)).NotNull();
        Guard.Argument(operationContext, nameof(operationContext)).NotNull();
        
        _catalogContext = catalogContext;
        _operationContext = operationContext;
    }
    
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();
        
        var product = await _catalogContext.Products.FirstOrDefaultAsync(
              product => product.Id == request.Id,
              cancellationToken)
            ?? throw new CatalogException($"Product with Id {request.Id} doesn't exist");

        product.Update(
            name: request.Name,
            description: request.Description,
            operationContext: _operationContext);
        
        _catalogContext.Products.Update(product);

        return await _catalogContext.SaveAsync(cancellationToken) > 0;
    }
}
