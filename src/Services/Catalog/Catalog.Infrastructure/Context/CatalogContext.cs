using Catalog.Application.Common.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Domain.Entities.Categories;
using Catalog.Domain.Entities.Combinations;
using Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalog.Infrastructure.Context;

public class CatalogContext : DbContext, ICatalogContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Combination> Combinations { get; set; }
    public DbSet<CombinationOption> CombinationOptions { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCombination> ProductCombinations { get; set; }
    public DbSet<ProductCombinationImage> ProductImages { get; set; }
    
    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
