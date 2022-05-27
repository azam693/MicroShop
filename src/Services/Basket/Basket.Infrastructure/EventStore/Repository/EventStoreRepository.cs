using Dawn;
using Kernel.Aggregates;
using Marten;

namespace Basket.Infrastructure.EventStore.Repository;

public class EventStoreRepository<T> : IEventStoreRepository<T>
    where T : Aggregate
{
    private readonly IDocumentSession _documentSession;

    public EventStoreRepository(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }
    
    public Task<T> Get(Guid id, CancellationToken cancellationToken)
    {
        Guard.Argument(id, nameof(id)).NotDefault();
        
        return (_documentSession.Events.AggregateStreamAsync<T>(id, token: cancellationToken)
            ?? throw new NullReferenceException($"There is no event with id {id}"))!;
    }

    public async Task<int> Add(T aggregate, CancellationToken cancellationToken)
    {
        Guard.Argument(aggregate, nameof(aggregate)).NotNull();
        
        var events = aggregate.DequeueEvents();
        _documentSession.Events.StartStream<Aggregate>(aggregate.Id, events);

        await _documentSession.SaveChangesAsync(cancellationToken);

        return events.Count;
    }
    
    public async Task<int> Update(T aggregate, CancellationToken cancellationToken)
    {
        Guard.Argument(aggregate, nameof(aggregate)).NotNull();
        
        var events = aggregate.DequeueEvents();
        _documentSession.Events.Append(
            aggregate.Id, 
            aggregate.Version,
            events);

        await _documentSession.SaveChangesAsync(cancellationToken);

        return events.Count;
    }

    public Task<int> Delete(T aggregate, CancellationToken cancellationToken)
    {
        return Update(aggregate, cancellationToken);
    }
}