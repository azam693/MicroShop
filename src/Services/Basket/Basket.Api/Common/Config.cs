using Basket.Domain.Common.Exceptions;
using Kernel.AspNet.Contexts;
using Kernel.Contexts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;

namespace Basket.Api.Common;

public static class Config
{
    private const string _version = "v1";
    
    public static void AddApi(this IServiceCollection services)
    {
        services.AddScoped<IOperationContext, HttpOperationContext>();
        services.AddHttpContextAccessor();
        
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc(_version, new OpenApiInfo { Title = "Basket API", Version = _version });
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            swagger.IncludeXmlComments(xmlPath);
        });
    }
    
    public static void UseApi(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler(options =>
        {
            options.Run(async context =>
            {
                var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionObject.Error;
                var statusCode = exception switch
                {
                    BasketException => HttpStatusCode.BadRequest,
                    _ => HttpStatusCode.InternalServerError
                };
                
                //logger.LogError(exception.Message, exception);
                context.Response.StatusCode = (int)statusCode;
            });
        });

        app.UseSwagger();
        app.UseSwaggerUI(swagger =>
        {
            swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket API " + _version);
        });
    }
}
