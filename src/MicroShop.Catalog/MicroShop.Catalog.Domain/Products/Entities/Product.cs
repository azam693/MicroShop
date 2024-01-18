using CommunityToolkit.Diagnostics;
using MicroShop.Catalog.Domain.Common;
using MicroShop.Catalog.Domain.Common.Models;

namespace MicroShop.Catalog.Domain.Products.Entities;

public sealed class Product : BaseEntity
{
    private List<ProductItem> _items = new List<ProductItem>();

    public string Name { get; private set; }
    public string? Description { get; private set; }

    public DateTime CreateDate { get; private set; }
    public Guid CreatedAt { get; private set; }

    public bool Published { get; private set; }
    public bool Deleted { get; private set; }

    public ProductCategory Category { get; private set; }
    
    public IReadOnlyCollection<ProductItem> Items => _items;

    public Product() { }

    public Product(
        string name,
        string? description,
        ProductItem item,
        ProductCategory category)
    {
        Guard.IsNotNullOrWhiteSpace(name);
        Guard.IsNotNull(item);
        Guard.IsNotNull(category);

        Id = Guid.NewGuid().ToString();
        Name = name;
        Description = description;
        CreatedAt = Guid.NewGuid();
        CreateDate = DateTime.UtcNow;

        Category = category;

        _items.Add(item);
    }

    public void Update(
        string name,
        string description)
    {
        Guard.IsNotNullOrWhiteSpace(name);

        Name = name;
        Description = description;
    }

    public void Publish()
    {
        if (!Items.Any())
        {
            throw new CatalogException("Product can't be published without combinations");
        }

        Published = true;
    }

    public void UnPublish()
    {
        Published = false;
    }

    public void Delete()
    {
        Deleted = true;
    }

    public void AddItem(ProductItem item)
    {
        Guard.IsNotNull(item);

        _items.Add(item);
    }

    public void DeleteItem(string sku)
    {
        Guard.IsNotNull(sku);

        var item = _items.FirstOrDefault(p => p.Sku == sku);
        if (item is null) 
        {
            throw new CatalogException($"Can't find item with sku = {sku}");
        }

        _items.Remove(item);
    }

    public void SetCategory(ProductCategory category)
    {
        Category = category;
    }
}
