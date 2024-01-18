using CommunityToolkit.Diagnostics;
using MicroShop.Profile.Domain.Addresses.Entities;
using MicroShop.Profile.Domain.Common.Services;
using System.Net.Mail;

namespace MicroShop.Profile.Domain.Users.Entities;

public sealed class User
{
    private List<Address> _addresses = new List<Address>();

    public int Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordSalt { get; private set; }
    public string PasswordHash { get; private set; }
    public IReadOnlyCollection<Address> Addresses => _addresses;

    private User() { }

    public User(string email, string password)
    {
        Guard.IsNotNullOrWhiteSpace(email);
        Guard.IsNotNullOrWhiteSpace(password);

        Email = new MailAddress(email).Address;
        PasswordSalt = PasswordHasher.GenerateSalt();
        PasswordHash = PasswordHasher.ComputeHash(password, PasswordSalt);
    }
}
