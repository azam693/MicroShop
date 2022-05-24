using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models.Responses;
using Dawn;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services.Categories.GetCategoryById;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
{
    private readonly ICatalogContext _catalogContext;
    
    public GetCategoryByIdHandler(
        ICatalogContext catalogContext)
    {
        Guard.Argument(catalogContext, nameof(catalogContext)).NotNull();

        _catalogContext = catalogContext;
    }
    
    public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var category = await _catalogContext.Categories.FirstOrDefaultAsync(
            category => category.Id == request.Id,
            cancellationToken);

        return new CategoryResponse(
            Id: category.Id,
            Name: category.Name,
            ParentId: category.ParentId);
    }
}
