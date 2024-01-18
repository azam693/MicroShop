using CommunityToolkit.Diagnostics;
using MicroShop.Order.Domain.Shipments.Entities;

namespace MicroShop.Order.Domain.Orders.Entities;

public sealed class Order
{
    private List<OrderItem> _items = new List<OrderItem>();
    private List<ShipmentAddress> _addresses = new List<ShipmentAddress>();

    public Guid Id { get; private set; }
    public OrderStatusEnum Status { get; private set; }
    public DateTime CreateDate { get; private set; }
    public DateTime UpdateDate { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items;
    public IReadOnlyCollection<ShipmentAddress> Addresss => _addresses;

    public Order(
        OrderStatusEnum status,
        IReadOnlyCollection<OrderItem> items,
        IReadOnlyCollection<ShipmentAddress> addresses)
    {
        Guard.IsNull(items);
        Guard.HasSizeLessThan(items, 0);
        
        Guard.IsNull(addresses);
        Guard.HasSizeLessThan(addresses, 0);

        Id = Guid.NewGuid();
        Status = status;

        _items.AddRange(items);
        _addresses.AddRange(addresses);
    }
}
