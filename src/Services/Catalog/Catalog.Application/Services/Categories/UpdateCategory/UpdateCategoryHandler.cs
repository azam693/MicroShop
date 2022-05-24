using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models.Responses;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Dawn;
using Kernel.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services.Categories.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ICatalogContext _catalogContext;
    private readonly IOperationContext _operationContext;
    
    public UpdateCategoryHandler(
        ICatalogContext catalogContext,
        IOperationContext operationContext)
    {
        Guard.Argument(catalogContext, nameof(catalogContext)).NotNull();
        Guard.Argument(operationContext, nameof(operationContext)).NotNull();
        
        _catalogContext = catalogContext;
        _operationContext = operationContext;
    }
    
    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();
        
        var category = await _catalogContext.Categories.FirstOrDefaultAsync(
                           category => category.Id == request.Id,
              cancellationToken)
            ?? throw new CatalogException($"Category with Id {request.Id} doesn't exist");

        category.Update(
            name: request.Name,
            parentId: request.ParentId,
            operationContext: _operationContext);
        
        _catalogContext.Categories.Update(category);

        return await _catalogContext.SaveAsync(cancellationToken) > 0;
    }
}
