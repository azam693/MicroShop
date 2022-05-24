using Catalog.Domain.Entities.Categories;
using Dawn;
using Kernel.Contexts;
using Kernel.Entities;

namespace Catalog.Domain.Entities.Products;

public class Product : GuidEntity
{
    private List<Category> _categories = new List<Category>();
    private List<ProductCombination> _productCombinations = new List<ProductCombination>();
    
    private Product() { }

    public Product(
        string name,
        string description,
        IOperationContext operationContext)
        : base(Guid.NewGuid())
    {
        Guard.Argument(operationContext, nameof(operationContext)).NotNull();
        
        Name = Guard.Argument(name).NotWhiteSpace();
        CreatedUserId = operationContext.UserId;
        Description = description;
        CreateDate = DateTime.UtcNow;
        ChangeDate = DateTime.UtcNow;
        ChangedUserId = CreatedUserId;
    }

    public string Name { get; protected set; }
    public string Description { get; protected set; }
    
    public DateTime CreateDate { get; protected set; }
    public DateTime ChangeDate { get; protected set; }
    public Guid CreatedUserId { get; protected set; }
    public Guid ChangedUserId { get; protected set; }

    public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
    
    public IReadOnlyCollection<ProductCombination> ProductCombinations => 
        _productCombinations.AsReadOnly();

    public void Update(
        string name,
        string description,
        IOperationContext operationContext)
    {
        Guard.Argument(operationContext, nameof(operationContext)).NotNull();
        
        Name = Guard.Argument(name, nameof(name)).NotWhiteSpace();
        Description = description;
        
        MarkChanged(operationContext.UserId);
    }

    public void AddProductCombination(
        ProductCombination productCombination,
        IOperationContext operationContext)
    {
        Guard.Argument(productCombination, nameof(productCombination)).NotNull();
        Guard.Argument(operationContext, nameof(operationContext)).NotNull();
        
        _productCombinations.Add(productCombination);
        
        MarkChanged(operationContext.UserId);
    }
    
    public void DeleteProductCombination(
        ProductCombination productCombination,
        IOperationContext operationContext)
    {
        Guard.Argument(productCombination, nameof(productCombination)).NotNull();
        Guard.Argument(operationContext, nameof(operationContext)).NotNull();
        
        _productCombinations.Remove(productCombination);
        
        MarkChanged(operationContext.UserId);
    }
    
    private void MarkChanged(Guid changeUserId)
    {
        ChangedUserId = changeUserId;
        ChangeDate = DateTime.UtcNow;
    }
}
