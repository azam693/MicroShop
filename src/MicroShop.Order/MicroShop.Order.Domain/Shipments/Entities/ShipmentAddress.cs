using CommunityToolkit.Diagnostics;

namespace MicroShop.Order.Domain.Shipments.Entities;

public sealed class ShipmentAddress
{
    public int AddressId { get; private set; }
    public string Value { get; private set; }

    public ShipmentAddress(int addressId, string value)
    {
        Guard.IsGreaterThan(addressId, 0);
        Guard.IsNotNullOrWhiteSpace(value);

        AddressId = addressId;
        Value = value;
    }
}
