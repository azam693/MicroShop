using MicroShop.Profile.Domain.Addresses.Entities;
using MicroShop.Profile.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.Profile.Domain.Common;

public interface IProfileContext
{
    DbSet<User> Users { get; set; }
    DbSet<Address> Addresses { get; set; }

    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
