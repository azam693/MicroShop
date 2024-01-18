using MicroShop.Catalog.Domain.Common.Models;

namespace MicroShop.Catalog.Domain.Common;

public interface IRepository<T>
    where T : BaseEntity
{
    public Task<T> GetAsync(string id, string? partitionKey, CancellationToken cancellationToken);
    public Task CreateAsync(T entity, string? partitionKey, CancellationToken cancellationToken);
    public Task UpdateAsync(T entity, string? partitionKey, CancellationToken cancellationToken);
    public Task DeleteAsync(string id, string? partitionKey, CancellationToken cancellationToken);
}
