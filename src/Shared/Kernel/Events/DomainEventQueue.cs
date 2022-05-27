namespace Kernel.Events;

public abstract class DomainEventQueue
{
    private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
    
    public IReadOnlyCollection<DomainEvent> DequeueEvents()
    {
        var dequeuedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();

        return dequeuedEvents;
    }
    
    protected void EnqueueEvent(DomainEvent domainEvent)
    {
        if (domainEvent is null)
            throw new ArgumentNullException(nameof(domainEvent));
        
        _domainEvents.Add(domainEvent);
    }
}