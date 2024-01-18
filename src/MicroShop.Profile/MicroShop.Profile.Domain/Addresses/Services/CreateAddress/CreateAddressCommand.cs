using CommunityToolkit.Diagnostics;
using MicroShop.Profile.Domain.Addresses.Entities;
using MicroShop.Profile.Domain.Common;
using MicroShop.Profile.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.Profile.Domain.Addresses.Services.CreateAddress;

internal sealed class CreateAddressCommand : ICreateAddressCommand
{
    private readonly IProfileContext _profileContext;

    public CreateAddressCommand(IProfileContext profileContext)
    {
        Guard.IsNotNull(profileContext);

        _profileContext = profileContext;
    }

    public async Task<int> Handle(CreateAddressRequest request, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(request);

        var user = await _profileContext.Users.FirstOrDefaultAsync(
            p => p.Id == request.UserId,
            cancellationToken);
        if (user is null)
        {
            throw new ProfileException($"Can't fine {nameof(User)} by id {request.UserId}");
        }

        var address = new Address(user, request.Value);

        _profileContext.Addresses.Add(address);

        await _profileContext.SaveChangesAsync(cancellationToken);

        return address.Id;
    }
}
