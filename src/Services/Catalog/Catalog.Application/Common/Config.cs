using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.Application.Common;

public static class Config
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}