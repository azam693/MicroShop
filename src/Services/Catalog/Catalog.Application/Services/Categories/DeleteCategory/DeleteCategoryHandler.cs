using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models.Responses;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Dawn;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services.Categories.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICatalogContext _catalogContext;
    
    public DeleteCategoryHandler(
        ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }
    
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request).NotNull();

        var category = await _catalogContext.Categories.FirstOrDefaultAsync(
                category => category.Id == request.Id,
                cancellationToken)
            ?? throw new CatalogException($"Category with Id {request.Id} doesn't exist");
        
        _catalogContext.Categories.Remove(category);

        return await _catalogContext.SaveAsync(cancellationToken) > 0;
    }
}
