namespace Kernel.Marten.Aggregates;

public abstract class Aggregate : Aggregate<Guid>, IAggregate
{
    
}

public abstract class Aggregate<T> : IAggregate<T>
    where T: notnull
{
    [NonSerialized]
    private readonly Queue<object> uncommittedEvents = new();

    public T Id { get; }
    public int Version { get; }
    
    public virtual void When(object @event)
    {
    }
    
    public object[] DequeueUncommittedEvents()
    {
        var dequeuedEvents = uncommittedEvents.ToArray();
        uncommittedEvents.Clear();

        return dequeuedEvents;
    }

    protected void Enqueue(object @event)
    {
        uncommittedEvents.Enqueue(@event);
    }
}