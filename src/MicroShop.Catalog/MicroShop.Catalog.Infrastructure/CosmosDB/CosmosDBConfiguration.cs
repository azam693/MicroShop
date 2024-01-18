using MicroShop.Catalog.Domain.Categories.Entities;
using MicroShop.Catalog.Domain.Products.Entities;
using Microsoft.Azure.Cosmos;

namespace MicroShop.Catalog.Infrastructure.CosmosDB;

internal static class CosmosDBConfiguration
{
    public static async Task<CosmosClient> Configure(CosmosSettings cosmosSettings)
    {
        var client = new CosmosClient(
            cosmosSettings.Account, 
            cosmosSettings.PrimaryKey,
            new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            });
        var database = await client.CreateDatabaseIfNotExistsAsync(cosmosSettings.DatabaseName);

        // Containers
        await database.Database.CreateContainerIfNotExistsAsync(
            nameof(Product),
            $"/{nameof(Product.Category).ToLower()}/{nameof(Product.Category.Name).ToLower()}");
        await database.Database.CreateContainerIfNotExistsAsync(
            nameof(Category),
            $"/{nameof(Category.Type).ToLower()}");

        return client;
    }
}
