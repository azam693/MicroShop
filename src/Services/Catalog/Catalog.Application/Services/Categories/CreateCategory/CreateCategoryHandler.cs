using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models.Responses;
using Catalog.Domain.Entities;
using Catalog.Domain.Entities.Categories;
using Catalog.Domain.Exceptions;
using Dawn;
using Kernel.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services.Categories.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly ICatalogContext _catalogContext;
    private readonly IOperationContext _operationContext;
    
    public CreateCategoryHandler(
        ICatalogContext catalogContext,
        IOperationContext operationContext)
    {
        _catalogContext = catalogContext;
        _operationContext = operationContext;
    }
    
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request).NotNull();

        var category = new Category(
            name: request.Name,
            operationContext: _operationContext,
            parentId: request.ParentId);
        _catalogContext.Categories.Add(category);

        bool isCreated = await _catalogContext.SaveAsync(cancellationToken) > 0;
        if (!isCreated)
        {
            throw new CatalogException(
                $"The category with name {request.Name} wasn't created");
        }

        return category.Id;
    }
}
