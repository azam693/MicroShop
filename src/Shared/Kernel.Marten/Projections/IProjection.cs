namespace Kernel.Marten.Projections;

public interface IProjection
{
    void When(object @event);
}
