using Catalog.Api.Common;
using Catalog.Api.Common.Middleware;
using Catalog.Application.Common;
using Catalog.Infrastructure.Common;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Catalog.Api;

public class Startup
{
    private const string _version = "v1";
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure(Configuration);
        services.AddApi();

        services.AddControllers();

        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc(_version, new OpenApiInfo { Title = "Catalog API", Version = _version });
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            swagger.IncludeXmlComments(xmlPath);
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(swagger =>
        {
            swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API " + _version);
        });
        
        app.UseRouting();
        app.UseAuthentication();
        //app.UseMiddleware<ExceptionMiddleware();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
