namespace MicroShop.Profile.Domain.Users.Services.CreateUser;

public interface ICreateUserCommand
{
    Task<int> Handle(CreateUserRequest request, CancellationToken cancellationToken);
}
