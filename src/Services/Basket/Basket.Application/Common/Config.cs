using Basket.Infrastructure.EventStore.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Basket.Application.Common;

public static class Config
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(
            typeof(IEventStoreRepository<>),
            typeof(EventStoreRepository<>));
    }
}
