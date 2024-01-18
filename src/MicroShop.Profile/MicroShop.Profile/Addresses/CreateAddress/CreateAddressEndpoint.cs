using MicroShop.Profile.Domain.Addresses.Entities;
using MicroShop.Profile.Domain.Addresses.Services.CreateAddress;
using MinimalApi.Endpoint;
using System.Net;

namespace MicroShop.Profile.Web.Addresses.CreateAddress;

public sealed class CreateAddressEndpoint : IEndpoint<IResult, CreateAddressHttpRequest, CancellationToken>
{
    private readonly ICreateAddressCommand _createAddressCommand;

    public CreateAddressEndpoint(ICreateAddressCommand createAddressCommand)
    {
        _createAddressCommand = createAddressCommand;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "api/address",
                (CreateAddressHttpRequest request, CancellationToken cancellationToken) 
                    => HandleAsync(request, cancellationToken))
            .Produces((int) HttpStatusCode.Created)
            .WithTags(nameof(Address))
            .RequireAuthorization();
    }

    public async Task<IResult> HandleAsync(CreateAddressHttpRequest request, CancellationToken cancellationToken)
    {
        int addressId = await _createAddressCommand.Handle(
            request.CreateAddressRequest(), 
            cancellationToken);

        return Results.Created($"api/user/{request.UserId}", null);
    }
}
