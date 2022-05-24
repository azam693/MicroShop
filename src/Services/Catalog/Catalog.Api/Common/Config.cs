using Catalog.Api.Common.Contexts;
using Kernel.Contexts;

namespace Catalog.Api.Common;

public static class Config
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddScoped<IOperationContext, HttpOperationContext>();
        services.AddHttpContextAccessor();
    }
}
