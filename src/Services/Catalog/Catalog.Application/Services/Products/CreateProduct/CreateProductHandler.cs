using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models.Responses;
using Catalog.Domain.Entities;
using Catalog.Domain.Entities.Products;
using Catalog.Domain.Exceptions;
using Dawn;
using Kernel.Contexts;
using MediatR;

namespace Catalog.Application.Services.Products.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly ICatalogContext _catalogContext;
    private readonly IOperationContext _operationContext;
    
    public CreateProductHandler(
        ICatalogContext catalogContext,
        IOperationContext operationContext)
    {
        Guard.Argument(catalogContext, nameof(catalogContext)).NotNull();
        Guard.Argument(operationContext, nameof(operationContext)).NotNull();
        
        _catalogContext = catalogContext;
        _operationContext = operationContext;
    }
    
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var product = new Product(
            name: request.Name,
            description: request.Description,
            operationContext: _operationContext);
        _catalogContext.Products.Add(product);
        bool isCreated = await _catalogContext.SaveAsync(cancellationToken) > 0;
        if (!isCreated)
        {
            throw new CatalogException(
                $"The product with name {request.Name} wasn't created");
        }
        
        return product.Id;
    }
}
