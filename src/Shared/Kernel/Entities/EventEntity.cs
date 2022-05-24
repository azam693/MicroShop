using Kernel.Events;

namespace Kernel.Entities;

public class EventEntity : DomainEventQueue, IEventEntity<int>
{
    public int Id { get; }
    public int Version { get; }
}
