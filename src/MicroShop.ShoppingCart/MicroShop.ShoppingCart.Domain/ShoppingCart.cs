using CommunityToolkit.Diagnostics;
using System.Text.Json.Serialization;

namespace MicroShop.ShoppingCart.Domain;

public sealed class ShoppingCart
{
    private List<ShoppingCartItem> _items = new List<ShoppingCartItem>();

    public string UserId { get; private set; }
    public IReadOnlyCollection<ShoppingCartItem> Items =>  _items;

    public ShoppingCart() { }

    public ShoppingCart(
        string userId,
        IEnumerable<ShoppingCartItem> items)
    {
        Guard.IsNotNullOrWhiteSpace(userId);

        UserId = userId;

        if (items is not null && items.Any())
        {
            _items.AddRange(items);
        }
    }

    public void Update(ShoppingCartItem item)
    {
        Guard.IsNotNull(item);

        var itemForRemove = _items.FirstOrDefault(p => p.ProductId == item.ProductId);
        if (itemForRemove is not null)
        {
            _items.Remove(itemForRemove);
        }

        _items.Add(item);
    }

    public void Remove(string productId)
    {
        Guard.IsNullOrWhiteSpace(productId);

        var itemForRemove = _items.FirstOrDefault(p => p.ProductId == productId);
        if (itemForRemove is null)
            return;

        _items.Remove(itemForRemove);
    }

    public decimal CalculateTotalPrice()
    {
        return _items.Sum(i => i.Price * i.Quantity);
    }
}
