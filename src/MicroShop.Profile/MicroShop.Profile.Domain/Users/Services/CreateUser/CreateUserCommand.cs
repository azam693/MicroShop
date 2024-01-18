using CommunityToolkit.Diagnostics;
using MicroShop.Profile.Domain.Common;
using MicroShop.Profile.Domain.Users.Entities;

namespace MicroShop.Profile.Domain.Users.Services.CreateUser;

internal sealed class CreateUserCommand : ICreateUserCommand
{
    private readonly IProfileContext _profileContext;

    public CreateUserCommand(IProfileContext profileContext)
    {
        Guard.IsNotNull(profileContext);

        _profileContext = profileContext;
    }

    public async Task<int> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(request);

        var user = new User(request.Email, request.Password);
        _profileContext.Users.Add(user);

        await _profileContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
