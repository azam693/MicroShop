using CommunityToolkit.Diagnostics;
using MicroShop.Catalog.Domain.Common.Models;

namespace MicroShop.Catalog.Domain.Categories.Entities;

public sealed class Category : BaseEntity
{
    public const string MainPartitionKey = "Main";

    public string Name { get; private set; }
    public string? ParentId { get; private set; }
    public string Type { get; set; }

    public Category() { }

    public Category(
        string name,
        string? parentId)
    {
        Guard.IsNotNullOrWhiteSpace(name);

        Id = Guid.NewGuid().ToString();
        Name = name;
        ParentId = parentId;
        Type = MainPartitionKey;
    }
}
