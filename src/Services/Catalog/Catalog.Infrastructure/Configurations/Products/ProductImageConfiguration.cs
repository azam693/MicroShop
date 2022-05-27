using Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Configurations.Products;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductCombinationImage>
{
    public void Configure(EntityTypeBuilder<ProductCombinationImage> builder)
    {
        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Path)
            .IsRequired()
            .HasMaxLength(500);
    }
}
