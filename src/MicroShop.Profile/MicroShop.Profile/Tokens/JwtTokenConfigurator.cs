using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace MicroShop.Profile.Web.Tokens;

public static class JwtTokenConfigurator
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtTokenConfig = configuration.GetSection("JwtToken");
        var jwtTokenSettings = jwtTokenConfig.Get<JwtTokenSettings>();
        services.Configure<JwtTokenSettings>(jwtTokenConfig);
        services
            .AddAuthentication()
            .AddJwtBearer(o =>
            {
                var rsa = RSA.Create();
                rsa.ImportFromPem(jwtTokenSettings.PublicKey);

                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new RsaSecurityKey(rsa),
                    ValidAudience = jwtTokenSettings.Audience,
                    ValidIssuer = jwtTokenSettings.Issuer,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                };
            });

        services.AddAuthorization();
    }

    public static void UseJwtAuthentication(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
