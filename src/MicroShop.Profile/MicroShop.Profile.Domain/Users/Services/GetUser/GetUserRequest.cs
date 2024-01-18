using CommunityToolkit.Diagnostics;

namespace MicroShop.Profile.Domain.Users.Services.GetUser;

public sealed class GetUserRequest
{
    public int Id { get; private set; }

    public GetUserRequest(int id)
    {
        Guard.IsGreaterThanOrEqualTo(id, 0);

        Id = id;
    }
}
