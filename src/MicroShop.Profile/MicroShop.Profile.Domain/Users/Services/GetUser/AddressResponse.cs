using MicroShop.Profile.Domain.Addresses.Entities;

namespace MicroShop.Profile.Domain.Users.Services.GetUser;

public sealed record AddressResponse(string Value)
{
    public static IReadOnlyCollection<AddressResponse> Create(IEnumerable<Address> addresses)
    {
        return addresses
            .Select(a => new AddressResponse(a.Value))
            .ToArray();
    }
}
