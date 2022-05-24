using Catalog.Domain.Entities.Combinations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Configurations.Combinations;

public class CombinationOptionConfiguration : IEntityTypeConfiguration<CombinationOption>
{
    public void Configure(EntityTypeBuilder<CombinationOption> builder)
    {
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Name)
            .IsRequired();
    }
}
