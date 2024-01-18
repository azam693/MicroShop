using MicroShop.ShoppingCart.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MicroShop.ShoppingCart.Infrastructure.JsonConverters;

internal sealed class ShoppingCartConverter : JsonConverter<Domain.ShoppingCart>
{
    public override Domain.ShoppingCart? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out JsonDocument jsonDoc))
        {
            throw new JsonException($"{nameof(ShoppingCartConverter)} couldn't parse {nameof(Domain.ShoppingCart)} JSON");
        }

        var shoppingCartJson = jsonDoc.RootElement;

        var userId = shoppingCartJson
            .GetProperty(nameof(Domain.ShoppingCart.UserId))
            .GetString();
        var items = shoppingCartJson
            .GetProperty(nameof(Domain.ShoppingCart.Items))
            .EnumerateArray()
            .Select(item =>
            {
                var productId = item
                    .GetProperty(nameof(ShoppingCartItem.ProductId))
                    .GetString();
                var quantity = item
                    .GetProperty(nameof(ShoppingCartItem.Quantity))
                    .GetInt32();
                var price = item
                    .GetProperty(nameof(ShoppingCartItem.Price))
                    .GetDecimal();

                return new ShoppingCartItem(productId, quantity, price);
            })
            .ToList();

        return new Domain.ShoppingCart(userId, items);
    }

    public override void Write(Utf8JsonWriter writer, Domain.ShoppingCart value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString(nameof(Domain.ShoppingCart.UserId), value.UserId);

        writer.WritePropertyName(nameof(Domain.ShoppingCart.Items));
        writer.WriteStartArray();
        foreach (var item in value.Items)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(ShoppingCartItem.ProductId), item.ProductId);
            writer.WriteNumber(nameof(ShoppingCartItem.Quantity), item.Quantity);
            writer.WriteNumber(nameof(ShoppingCartItem.Price), item.Price);

            writer.WriteEndObject();
        }
        writer.WriteEndArray();

        writer.WriteEndObject();
    }
}
