using MicroShop.Profile.Domain.Addresses.Services.CreateAddress;
using System.ComponentModel.DataAnnotations;

namespace MicroShop.Profile.Web.Addresses.CreateAddress;

public sealed record CreateAddressHttpRequest
{
    public int UserId { get; init; }

    [Required]
    public string Value { get; init; }

    public CreateAddressRequest CreateAddressRequest()
    {
        return new CreateAddressRequest(UserId, Value);
    }
}
