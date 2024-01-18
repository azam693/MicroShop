using MicroShop.Catalog.Domain.Categories.Entities;
using MicroShop.Catalog.Domain.Categories.Services.GetCategory;
using MinimalApi.Endpoint;

namespace MicroShop.Catalog.Web.Categories.GetCategory;

public sealed class GetCategoryEndpoint : IEndpoint<IResult, GetCategoryHttpRequest, CancellationToken>
{
    private readonly IGetCategoryQuery _getCategoryQuery;

    public GetCategoryEndpoint(IGetCategoryQuery getCategoryQuery)
    {
        _getCategoryQuery = getCategoryQuery;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/category/{id}", 
            ([AsParameters]GetCategoryHttpRequest request, CancellationToken cancellationToken) 
                => HandleAsync(request, cancellationToken))
            .Produces<GetCategoryResponse>()
            .WithTags(nameof(Category));
    }

    public async Task<IResult> HandleAsync(GetCategoryHttpRequest request, CancellationToken cancellationToken)
    {
        var response = await _getCategoryQuery.Handle(request.CreateServiceRequest(), cancellationToken);

        return Results.Ok(response);
    }
}
