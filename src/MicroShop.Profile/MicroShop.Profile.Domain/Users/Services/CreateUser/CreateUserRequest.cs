using CommunityToolkit.Diagnostics;

namespace MicroShop.Profile.Domain.Users.Services.CreateUser;

public sealed class CreateUserRequest
{
    public string Email { get; private set; }
    public string Password { get; private set; }

    public CreateUserRequest(string email, string password)
    {
        Guard.IsNotNull(email);
        Guard.IsNotNull(password);

        Email = email.Trim();
        Password = password;
    }
}
