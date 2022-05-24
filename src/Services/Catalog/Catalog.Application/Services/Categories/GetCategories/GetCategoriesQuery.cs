using Catalog.Application.Common.Models.Responses;
using Dawn;
using Kernel.Exceptions;
using MediatR;
using System.Drawing;

namespace Catalog.Application.Services.Categories.GetCategories;

public record GetCategoriesQuery : IRequest<IEnumerable<CategoryResponse>>
{
    public GetCategoriesQuery(
        int pageNumber,
        int pageSize = 20)
    {
        PageNumber = Guard.Argument(pageNumber, nameof(pageNumber)).GreaterThan(0);
        PageSize = Guard.Argument(pageSize, nameof(pageSize)).GreaterThan(0);
    }

    public int PageNumber { get; }
    public int PageSize { get; }
}
