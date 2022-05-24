using Catalog.Domain.Entities.Combinations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Configurations.Combinations;

public class CombinationConfiguration : IEntityTypeConfiguration<Combination>
{
    public void Configure(EntityTypeBuilder<Combination> builder)
    {
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Name)
            .IsRequired();
    }
}
