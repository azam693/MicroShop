using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace MicroShop.Catalog.Infrastructure.CosmosDB;

internal sealed class CosmosContainerFactory
{
    private readonly CosmosClient _cosmosClient;
    private readonly CosmosSettings _cosmosSettings;

    public CosmosContainerFactory(
        CosmosClient cosmosClient,
        IOptions<CosmosSettings> cosmosSettings)
    {
        _cosmosClient = cosmosClient;
        _cosmosSettings = cosmosSettings.Value;
    }

    public Container Get<T>()
    {
        return _cosmosClient.GetContainer(_cosmosSettings.DatabaseName, typeof(T).Name);
    }
}
