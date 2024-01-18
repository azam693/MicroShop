using MicroShop.Profile.Domain.Addresses.Entities;
using MicroShop.Profile.Domain.Users.Entities;

namespace MicroShop.Profile.Domain.Users.Services.GetUser;

public sealed record GetUserResponse(int Id, string Email, IReadOnlyCollection<AddressResponse> Addresses)
{
    public static GetUserResponse Create(User user)
    {
        return new GetUserResponse(
            user.Id, 
            user.Email, 
            AddressResponse.Create(user.Addresses ?? Array.Empty<Address>()));
    }
}
