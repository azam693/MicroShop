using MicroShop.Profile.Domain.Users.Entities;
using MicroShop.Profile.Domain.Users.Services.GetUser;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MinimalApi.Endpoint;

namespace MicroShop.Profile.Web.Users.GetUser;

public sealed class GetUserEndpoint : IEndpoint<GetUserResponse, int, CancellationToken>
{
    private readonly IGetUserQuery _getUserQuery;

    public GetUserEndpoint(IGetUserQuery getUserQuery)
    {
        _getUserQuery = getUserQuery;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "api/user/{id}", 
                (int id, CancellationToken cancellationToken) => HandleAsync(id, cancellationToken))
            .Produces<GetUserResponse>()
            .WithTags(nameof(User))
            .RequireAuthorization();
    }

    public Task<GetUserResponse> HandleAsync(int id, CancellationToken cancellationToken)
    {
        return _getUserQuery.Handle(new GetUserRequest(id), cancellationToken);
    }
}
