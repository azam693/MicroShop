using MicroShop.Catalog.Domain.Common;
using MicroShop.Catalog.Infrastructure.CosmosDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroShop.Catalog.Infrastructure;

public static class DependencyResolver
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureCosmosDB(services, configuration);
    }

    private static void ConfigureCosmosDB(IServiceCollection services, IConfiguration configuration)
    {
        var cosmosSection = configuration.GetSection("CosmosDB");
        var cosmosClient = CosmosDBConfiguration.Configure(cosmosSection.Get<CosmosSettings>())
            .GetAwaiter()
            .GetResult();

        services.AddSingleton(cosmosClient);
        services.Configure<CosmosSettings>(cosmosSection);
        services.AddSingleton<CosmosContainerFactory>();
        services.AddScoped(typeof(IRepository<>), typeof(CosmosRepository<>));
    }
}
