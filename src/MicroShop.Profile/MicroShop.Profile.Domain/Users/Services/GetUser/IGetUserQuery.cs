namespace MicroShop.Profile.Domain.Users.Services.GetUser;

public interface IGetUserQuery
{
    Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken);
}
