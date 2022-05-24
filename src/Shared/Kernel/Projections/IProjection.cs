namespace Kernel.Projections;

public interface IProjection
{
    void When(object @event);
}
