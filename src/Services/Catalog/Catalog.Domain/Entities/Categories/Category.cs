using Catalog.Domain.Entities.Products;
using Dawn;
using Kernel.Contexts;
using Kernel.Entities;

namespace Catalog.Domain.Entities.Categories;

public class Category : GuidEntity
{
    private Category() { }

    public Category(
        string name,
        IOperationContext operationContext,
        Guid? parentId = null)
        : base(Guid.NewGuid())
    {
        Guard.Argument(operationContext, nameof(operationContext)).NotNull();
        
        Name = Guard.Argument(name, nameof(name)).NotWhiteSpace();
        CreatedUserId = operationContext.UserId;
        ParentId = parentId;
        CreateDate = DateTime.UtcNow;
        ChangeDate = DateTime.UtcNow;
        ChangedUserId = CreatedUserId;
    }
    
    public string Name { get; protected set; }
    public Guid? ParentId { get; protected set; }

    public DateTime CreateDate { get; protected set; }
    public DateTime ChangeDate { get; protected set; }
    public Guid CreatedUserId { get; protected set; }
    public Guid ChangedUserId { get; protected set; }
    
    public IReadOnlyCollection<Product> Products { get; protected set; }
        = Array.Empty<Product>();
    
    public void Update(
        string name,
        Guid? parentId,
        IOperationContext operationContext)
    {
        Guard.Argument(operationContext, nameof(operationContext)).NotNull();
        
        Name = Guard.Argument(name, nameof(name)).NotWhiteSpace();
        ParentId = parentId;
        
        MarkChanged(operationContext.UserId);
    }

    private void MarkChanged(Guid changeUserId)
    {
        ChangedUserId = changeUserId;
        ChangeDate = DateTime.UtcNow;
    }
}
