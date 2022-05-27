namespace Kernel.Aggregates;

public interface IAggregate<T>
{
    T Id { get; }
    int Version { get; }
}
