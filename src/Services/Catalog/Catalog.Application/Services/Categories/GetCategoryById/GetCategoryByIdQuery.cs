using Catalog.Application.Common.Models.Responses;
using Dawn;
using MediatR;

namespace Catalog.Application.Services.Categories.GetCategoryById;

public record GetCategoryByIdQuery : IRequest<CategoryResponse>
{
    public GetCategoryByIdQuery(Guid id)
    {
        Id = Guard.Argument(id, nameof(id)).NotDefault();
    }

    public Guid Id { get; }
}
