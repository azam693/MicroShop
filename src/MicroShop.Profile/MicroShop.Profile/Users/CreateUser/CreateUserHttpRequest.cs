using MicroShop.Profile.Domain.Users.Services.CreateUser;
using System.ComponentModel.DataAnnotations;

namespace MicroShop.Profile.Web.Users.CreateUser;

public sealed record CreateUserHttpRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required(AllowEmptyStrings = false)]
    public string Password { get; init; }

    public CreateUserRequest CreateUserRequest()
    {
        return new CreateUserRequest(Email, Password);
    }
}
