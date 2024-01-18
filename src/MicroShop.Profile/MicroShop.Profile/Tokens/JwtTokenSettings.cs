namespace MicroShop.Profile.Web.Tokens;

public sealed class JwtTokenSettings
{
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string PublicKey { get; init; }
    public string PrivateKey { get; init; }
}
