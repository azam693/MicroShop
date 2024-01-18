using MicroShop.Profile.Domain.Addresses.Services.CreateAddress;
using MicroShop.Profile.Domain.Users.Services.CreateUser;
using MicroShop.Profile.Domain.Users.Services.GetUser;
using Microsoft.Extensions.DependencyInjection;

namespace MicroShop.Profile.Domain;

public static class DependencyResolver
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddScoped<ICreateAddressCommand, CreateAddressCommand>();
        services.AddScoped<ICreateUserCommand, CreateUserCommand>();
        services.AddScoped<IGetUserQuery, GetUserQuery>();
    }
}
