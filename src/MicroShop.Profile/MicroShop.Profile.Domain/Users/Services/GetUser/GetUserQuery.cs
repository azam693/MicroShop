using CommunityToolkit.Diagnostics;
using MicroShop.Profile.Domain.Common;
using MicroShop.Profile.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.Profile.Domain.Users.Services.GetUser;

internal sealed class GetUserQuery : IGetUserQuery
{
    private readonly IProfileContext _profileContext;

    public GetUserQuery(IProfileContext profileContext)
    {
        Guard.IsNotNull(profileContext);

        _profileContext = profileContext;
    }

    public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(request);

        var user = await _profileContext.Users
            .Include(u => u.Addresses)
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
        if (user is null)
        {
            throw new ProfileException($"Can't fine {nameof(User)} by id {request.Id}");
        }

        return GetUserResponse.Create(user);
    }
}
