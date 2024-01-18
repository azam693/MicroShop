using MicroShop.ShoppingCart.Domain.Common;
using MicroShop.ShoppingCart.Infrastructure.JsonConverters;
using StackExchange.Redis;
using System.Text.Json;

namespace MicroShop.ShoppingCart.Infrastructure.Redis;

internal class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly IDatabase _database;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ShoppingCartRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        _jsonSerializerOptions = new JsonSerializerOptions();
        _jsonSerializerOptions.Converters.Add(new ShoppingCartConverter());

        _database = connectionMultiplexer.GetDatabase();
    }

    public async Task<Domain.ShoppingCart?> GetAsync(string userId)
    {
        var redisValue = await _database.StringGetAsync(GetKey(userId));
        if (string.IsNullOrWhiteSpace(redisValue))
            return null;

        return JsonSerializer.Deserialize<Domain.ShoppingCart>(
            redisValue.ToString(),
            _jsonSerializerOptions);
    }

    public Task CreateOrUpdateAsync(Domain.ShoppingCart cart)
    {
        var json = JsonSerializer.Serialize(cart, _jsonSerializerOptions);

        return _database.StringSetAsync(GetKey(cart.UserId), json);
    }

    public Task DeleteAsync(string userId)
    {
        return _database.KeyDeleteAsync(GetKey(userId));
    }

    private string GetKey(string id)
    {
        return $"cart:{id}";
    }
}
