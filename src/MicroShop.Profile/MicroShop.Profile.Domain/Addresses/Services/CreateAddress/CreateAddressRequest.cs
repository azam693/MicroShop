using CommunityToolkit.Diagnostics;

namespace MicroShop.Profile.Domain.Addresses.Services.CreateAddress;

public sealed class CreateAddressRequest
{
    public int UserId { get; private set; }
    public string Value { get; private set; }

    public CreateAddressRequest(int userId, string value)
    {
        Guard.IsGreaterThanOrEqualTo(userId, 0);
        Guard.IsNotNullOrWhiteSpace(value);

        UserId = userId;
        Value = value;
    }
}
