using Kernel.Marten.Projections;

namespace Kernel.Marten.Aggregates;

public interface IAggregate : IAggregate<Guid>
{
    
}

public interface IAggregate<out T> : IProjection
{
    T Id { get; }
    int Version { get; }
    object[] DequeueUncommittedEvents();
}
