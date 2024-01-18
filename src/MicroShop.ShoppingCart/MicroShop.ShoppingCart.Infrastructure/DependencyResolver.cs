using MicroShop.ShoppingCart.Domain.Common;
using MicroShop.ShoppingCart.Infrastructure.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MicroShop.ShoppingCart.Infrastructure;

public static class DependencyResolver
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("Redis");

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
    }
}
