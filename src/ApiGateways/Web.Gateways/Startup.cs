using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Web.Gateway;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOcelot();
    }

    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Hello world!");
            });
        });

        await app.UseOcelot();
    }
}
