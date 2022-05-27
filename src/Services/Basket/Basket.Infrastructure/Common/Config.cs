using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weasel.Core;

namespace Basket.Infrastructure.Common;

public static class Config
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMarten(options =>
        {
            string connectionString = configuration.GetConnectionString("EventStore");
            
            options.Connection(connectionString);
            options.AutoCreateSchemaObjects = AutoCreate.All;
            options.CreateDatabasesForTenants(db =>
            {
                db.MaintenanceDatabase(connectionString);
                db
                    .ForTenant()
                    .CheckAgainstPgDatabase()
                    .WithOwner("postgres")
                    .WithEncoding("UTF-8")
                    .ConnectionLimit(-1);
            });
        });
    }
}
