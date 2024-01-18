namespace MicroShop.Profile.Domain.Addresses.Services.CreateAddress;

public interface ICreateAddressCommand
{
    Task<int> Handle(CreateAddressRequest request, CancellationToken cancellationToken);
}
