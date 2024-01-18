using Azure;
using MicroShop.Catalog.Domain.Common;
using MicroShop.Catalog.Domain.Common.Models;
using Microsoft.Azure.Cosmos;

namespace MicroShop.Catalog.Infrastructure.CosmosDB;

internal class CosmosRepository<T> : IRepository<T>
    where T : BaseEntity
{
    protected readonly Container _baseContainer;

    public CosmosRepository(CosmosContainerFactory cosmosContainerFactory)
    {
        _baseContainer = cosmosContainerFactory.Get<T>();
    }

    public async Task<T> GetAsync(
        string id,
        string? partitionKey,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(partitionKey))
        {
            return await GetWithoutPartition(id, cancellationToken);
        }
        else
        {
            var response = await _baseContainer.ReadItemAsync<T>(
                id,
                GetPartitionKey(partitionKey),
                cancellationToken: cancellationToken);

            return response.Resource;
        }
    }

    private async Task<T?> GetWithoutPartition(string id, CancellationToken cancellationToken)
    {
        string tableName = typeof(T).Name;
        var query = _baseContainer.GetItemQueryIterator<T>(
            $"SELECT * FROM {tableName} t WHERE t.id = \"{id}\"");
        if (query.HasMoreResults)
        {
            var document = await query.ReadNextAsync();

            return document.Resource.FirstOrDefault();
        }

        return null;
    }

    public Task CreateAsync(
        T entity,
        string? partitionKey,
        CancellationToken cancellationToken)
    {
        return _baseContainer.CreateItemAsync(
            entity, 
            GetPartitionKey(partitionKey), 
            cancellationToken: cancellationToken);
    }

    public Task UpdateAsync(
        T entity,
        string? partitionKey,
        CancellationToken cancellationToken)
    {
        return _baseContainer.ReplaceItemAsync(
            entity, 
            entity.Id,
            GetPartitionKey(partitionKey), 
            cancellationToken: cancellationToken);
    }

    public Task DeleteAsync(
        string id,
        string? partitionKey,
        CancellationToken cancellationToken)
    {
        return _baseContainer.DeleteItemAsync<T>(
            id, 
            GetPartitionKey(partitionKey), 
            cancellationToken: cancellationToken);
    }

    private PartitionKey GetPartitionKey(string? partitionKey)
    {
        return partitionKey is null
            ? PartitionKey.None
            : new PartitionKey(partitionKey);
    }
}
