using MicroShop.Profile.Domain.Addresses.Entities;
using MicroShop.Profile.Domain.Common;
using MicroShop.Profile.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.Profile.Infrastructure.SQLServer;

public sealed class ProfileContext : DbContext, IProfileContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public ProfileContext(DbContextOptions<ProfileContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
