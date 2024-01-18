namespace MicroShop.ShoppingCart.Domain.Common;

public interface IShoppingCartRepository
{
    public Task<ShoppingCart?> GetAsync(string userId);
    public Task CreateOrUpdateAsync(ShoppingCart cart);
    public Task DeleteAsync(string userId);
}
