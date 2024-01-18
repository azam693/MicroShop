using MinimalApi.Endpoint.Extensions;
using MicroShop.Profile.Domain;
using MicroShop.Profile.Infrastructure;
using Microsoft.OpenApi.Models;
using MicroShop.Profile.Web.Common.Middlewares;
using MicroShop.Profile.Web.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDomain();

builder.Services.AddEndpoints();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value == "/")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }

    await next.Invoke();
});

app.UseJwtAuthentication();

app.Run();
