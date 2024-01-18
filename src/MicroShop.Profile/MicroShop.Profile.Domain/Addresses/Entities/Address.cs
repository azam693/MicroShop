using CommunityToolkit.Diagnostics;
using MicroShop.Profile.Domain.Users.Entities;

namespace MicroShop.Profile.Domain.Addresses.Entities;

public sealed class Address
{
    public int Id { get; private set; }
    public string Value { get; private set; }
    public User User { get; private set; }

    private Address() { }

    public Address(User user, string value)
    {
        Guard.IsNotNull(user);
        Guard.IsNotNullOrWhiteSpace(value);

        User = user;
        Value = value;
    }
}
