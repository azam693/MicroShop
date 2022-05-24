namespace Kernel.Entities;

public interface IEventEntity<T> : IEntity<T>
{
    int Version { get; }
}
