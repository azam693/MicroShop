using MicroShop.Profile.Domain.Users.Entities;
using MicroShop.Profile.Domain.Users.Services.CreateUser;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;
using System.ComponentModel.DataAnnotations;
using System;
using System.Net;

namespace MicroShop.Profile.Web.Users.CreateUser;

public sealed class CreateUserEndpoint : IEndpoint<IResult, CreateUserHttpRequest, CancellationToken>
{
    private readonly ICreateUserCommand _createUserCommand;

    public CreateUserEndpoint(ICreateUserCommand createUserCommand)
    {
        _createUserCommand = createUserCommand;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "api/user",
                ([FromBody] CreateUserHttpRequest request, CancellationToken cancellationToken)
                    => HandleAsync(request, cancellationToken))
            .Produces((int)HttpStatusCode.Created)
            .WithTags(nameof(User));
    }

    public async Task<IResult> HandleAsync(CreateUserHttpRequest request, CancellationToken cancellationToken)
    {
        int userId = await _createUserCommand.Handle(request.CreateUserRequest(), cancellationToken);

        return Results.Created($"api/user/{userId}", null);
    }
}
