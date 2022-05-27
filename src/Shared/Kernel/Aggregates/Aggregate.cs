using Kernel.Events;

namespace Kernel.Aggregates;

public class Aggregate : DomainEventQueue, IAggregate<Guid>
{
    public Guid Id { get; protected set; }
    public int Version { get; protected set; }
}
