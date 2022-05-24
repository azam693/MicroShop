namespace Kernel.Entities;

public class GuidEntity : IEntity<Guid>
{
    protected GuidEntity()
    {
        
    }
    
    public GuidEntity(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; }
}
