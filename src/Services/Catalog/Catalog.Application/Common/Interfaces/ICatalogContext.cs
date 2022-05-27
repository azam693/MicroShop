using Catalog.Domain.Entities;
using Catalog.Domain.Entities.Categories;
using Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Common.Interfaces;

public interface ICatalogContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductCombinationImage> ProductImages { get; set; }

    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}
