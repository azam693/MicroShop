using MinimalApi.Endpoint.Extensions;
using MicroShop.Catalog.Domain;
using MicroShop.Catalog.Infrastructure;
using Microsoft.OpenApi.Models;
using MicroShop.Catalog.Domain.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDomain();

builder.Services.AddEndpoints();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapEndpoints();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
});

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value == "/")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }

    await next.Invoke();
});

app.Run();
