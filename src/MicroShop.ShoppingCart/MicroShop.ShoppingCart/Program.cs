using MicroShop.ShoppingCart.Domain.Common;
using MicroShop.ShoppingCart.Infrastructure;
using MicroShop.ShoppingCart.Domain;
using Microsoft.OpenApi.Models;
using MicroShop.ShoppingCart.Web.Common;
using MicroShop.ShoppingCart.Web.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

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

app.MapGet("/cart", async (string userId, IShoppingCartRepository shoppingCartRepository) =>
{
    var shoppingCart = await shoppingCartRepository.GetAsync(userId);

    return ShoppingCartResponse.Create(shoppingCart);
});

app.MapPut("/cart/{userId}/item", async (string userId, [FromBody] ShoppingCartItemHttpRequest request, IShoppingCartRepository shoppingCartRepository) =>
{
    var shoppingCart = await shoppingCartRepository.GetAsync(userId)
        ?? new ShoppingCart(userId, Array.Empty<ShoppingCartItem>());

    shoppingCart.Update(request.CreateShoppingCartItem());

    await shoppingCartRepository.CreateOrUpdateAsync(shoppingCart);

    return ShoppingCartResponse.Create(shoppingCart);
});

app.MapDelete("/cart/{userId}/item/{productId}", async (string userId, string productId, IShoppingCartRepository shoppingCartRepository) =>
{
    var shoppingCart = await shoppingCartRepository.GetAsync(userId)
        ?? new ShoppingCart(userId, Array.Empty<ShoppingCartItem>());
    shoppingCart.Remove(productId);

    await shoppingCartRepository.CreateOrUpdateAsync(shoppingCart);

    return ShoppingCartResponse.Create(shoppingCart);
});

app.MapDelete("/cart/{userId}", async (string userId, IShoppingCartRepository shoppingCartRepository) =>
{
    await shoppingCartRepository.DeleteAsync(userId);

    return ShoppingCartResponse.Create(new ShoppingCart(userId, Array.Empty<ShoppingCartItem>()));
});

app.Run();
