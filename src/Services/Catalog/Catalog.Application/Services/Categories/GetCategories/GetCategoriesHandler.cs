using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models.Responses;
using Dawn;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services.Categories.GetCategories;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryResponse>>
{
    private readonly ICatalogContext _catalogContext;
    
    public GetCategoriesHandler(
        ICatalogContext catalogContext)
    {
        Guard.Argument(catalogContext, nameof(catalogContext)).NotNull();
        
        _catalogContext = catalogContext;
    }
    
    public async Task<IEnumerable<CategoryResponse>> Handle(
        GetCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var categoryQuery = _catalogContext.Categories
            .Skip(request.PageNumber * request.PageSize)
            .Take(request.PageSize);
        var categories = await categoryQuery.ToListAsync(cancellationToken);

        return categories.Select(
            category => new CategoryResponse(
                Id: category.Id,
                Name: category.Name,
                ParentId: category.ParentId));
    }
}
