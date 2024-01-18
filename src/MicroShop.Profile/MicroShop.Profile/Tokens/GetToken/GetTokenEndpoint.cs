using MicroShop.Profile.Domain.Users.Services.GetUser;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Endpoint;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MicroShop.Profile.Web.Tokens.GetToken;

public sealed class GetTokenEndpoint : IEndpoint<IResult, int, CancellationToken>
{
    private readonly IGetUserQuery _getUserQuery;
    private readonly JwtTokenSettings _jwtSettings;

    public GetTokenEndpoint(IGetUserQuery getUserQuery, IOptions<JwtTokenSettings> jwtSettings)
    {
        _getUserQuery = getUserQuery;
        _jwtSettings = jwtSettings.Value;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/token", (int userId, CancellationToken cancellationToken)
                => HandleAsync(userId, cancellationToken))
            .Produces((int)HttpStatusCode.OK)
            .WithTags("Token");
    }

    public async Task<IResult> HandleAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await _getUserQuery.Handle(new GetUserRequest(userId), cancellationToken);
        if (user is null)
        {
            return Results.Unauthorized();
        }

        string jwtToken = GenerateToken(user);

        return Results.Ok(jwtToken);
    }

    private string GenerateToken(GetUserResponse user)
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(_jwtSettings.PrivateKey);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email)
            }),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new RsaSecurityKey(rsa),
                SecurityAlgorithms.RsaSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
