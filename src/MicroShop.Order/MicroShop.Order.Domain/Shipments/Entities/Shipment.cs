using CommunityToolkit.Diagnostics;

namespace MicroShop.Order.Domain.Shipments.Entities;

public sealed class Shipment
{
    public Guid Id { get; private set; }
    public TransportCompanyEnum TransportCompany { get; private set; }
    public ShipmentStatusEnum ShipmentStatus { get; private set; }
    public ShipmentAddress AddressesFrom { get; private set; }
    public ShipmentAddress AddressesTo { get; private set; }

    public Shipment(
        TransportCompanyEnum transportCompany,
        ShipmentStatusEnum shipmentStatus,
        ShipmentAddress addressFrom,
        ShipmentAddress addressTo)
    {
        Guard.IsNotNull(addressFrom);
        Guard.IsNotNull(addressTo);

        TransportCompany = transportCompany;
        ShipmentStatus = shipmentStatus;
        AddressesFrom = addressFrom;
        AddressesTo = addressTo;
    }
}
