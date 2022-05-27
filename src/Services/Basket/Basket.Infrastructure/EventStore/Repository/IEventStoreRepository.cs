using Kernel.Aggregates;

namespace Basket.Infrastructure.EventStore.Repository;

public interface IEventStoreRepository<T> where T : Aggregate
{
    Task<T> Get(Guid id, CancellationToken cancellationToken);
    Task<int> Add(T aggregate, CancellationToken cancellationToken);
    Task<int> Update(T aggregate, CancellationToken cancellationToken);
    Task<int> Delete(T aggregate, CancellationToken cancellationToken);
}
