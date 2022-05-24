using Catalog.Application.Common.Interfaces;
using Catalog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.Common;

public static class Config
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("CommonConnection");
        services.AddDbContext<CatalogContext>(
            options => options.UseNpgsql(connectionString));
        services.AddTransient<ICatalogContext, CatalogContext>();
    }
}
