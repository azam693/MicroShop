using Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Configurations.Products;

public class ProductCombinationConfiguration : IEntityTypeConfiguration<ProductCombination>
{
    public void Configure(EntityTypeBuilder<ProductCombination> builder)
    {
        builder.HasKey(product => product.Id);
        builder.Property(product => product.CombinationOptionIds)
            .HasColumnType("jsonb")
            .IsRequired();
    }
}
