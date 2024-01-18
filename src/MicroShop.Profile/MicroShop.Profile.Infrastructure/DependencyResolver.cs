using MicroShop.Profile.Domain.Common;
using MicroShop.Profile.Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroShop.Profile.Infrastructure;

public static class DependencyResolver
{
    public static void AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("ProfileConnection");
        services.AddDbContext<ProfileContext>(o => o.UseSqlServer(connectionString));

        services.AddScoped<IProfileContext, ProfileContext>();
    }
}
