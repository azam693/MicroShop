using Basket.Domain.ShoppringCarts.Events;
using Dawn;
using Kernel.Aggregates;
using Kernel.Contexts;

namespace Basket.Domain.ShoppringCarts;

public class ShoppingCart : Aggregate
{
    private List<ShoppingCartItem> _shoppingCartItems = new List<ShoppingCartItem>();
    
    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            foreach (var shoppingCartItem in ShoppingCartItems)
            {
                totalPrice += shoppingCartItem.Price * shoppingCartItem.Quantity;
            }

            return totalPrice;
        }
    }

    private ShoppingCart() { }

    public IReadOnlyCollection<ShoppingCartItem> ShoppingCartItems 
        => _shoppingCartItems.AsReadOnly();
    
    public ShoppingCart(
        ShoppingCartItem shoppingCartItem,
        IOperationContext operationContext)
    {
        Guard.Argument(operationContext, nameof(operationContext)).NotNull();
        Guard.Argument(shoppingCartItem, nameof(shoppingCartItem)).NotNull();
        
        var shoppingCartInitialized = new ShoppingCartInitialized(
            id: Guid.NewGuid(),
            shoppingCartItem: shoppingCartItem,
            operationContext: operationContext);
        
        EnqueueEvent(shoppingCartInitialized);
        Apply(shoppingCartInitialized);
    }

    private void Apply(ShoppingCartInitialized shoppingCartInitialized)
    {
        Version++;

        Id = shoppingCartInitialized.Id;
        _shoppingCartItems.Add(shoppingCartInitialized.ShoppingCartItem);
    }
    
    public void AddItem(
        ShoppingCartItem shoppingCartItem,
        IOperationContext operationContext)
    {
        Guard.Argument(shoppingCartItem, nameof(shoppingCartItem)).NotNull();

        var shoppingCartItemAdded = new ShoppingCartItemAdded(
            shoppingCartItem: shoppingCartItem,
            operationContext: operationContext);
        
        EnqueueEvent(shoppingCartItemAdded);
        Apply(shoppingCartItemAdded);
    }

    private void Apply(ShoppingCartItemAdded @event)
    {
        Version++;

        _shoppingCartItems.Add(@event.ShoppingCartItem);
    }

    public void UpdateProductItem(
        ShoppingCartItem shoppingCartItem,
        IOperationContext operationContext)
    {
        Guard.Argument(shoppingCartItem, nameof(shoppingCartItem)).NotNull();

        var shoppingCartItemUpdated = new ShoppingCartItemUpdated(
            shoppingCartItem: shoppingCartItem,
            operationContext: operationContext);
        
        EnqueueEvent(shoppingCartItemUpdated);
        Apply(shoppingCartItemUpdated);
    }
    
    private void Apply(ShoppingCartItemUpdated @event)
    {
        Version++;

        var shoppingCartItem = _shoppingCartItems.First(
            item => item.ProductCombinationId == @event.ShoppingCartItem.ProductCombinationId);
        _shoppingCartItems.Remove(shoppingCartItem);
        
        _shoppingCartItems.Add(new ShoppingCartItem(
            productCombinationId: @event.ShoppingCartItem.ProductCombinationId,
            quantity: @event.ShoppingCartItem.Quantity,
            price: @event.ShoppingCartItem.Price));
    }
    
    public void DeleteItem(
        Guid productCombinationId,
        IOperationContext operationContext)
    {
        Guard.Argument(productCombinationId, nameof(productCombinationId)).NotDefault();

        var shoppingCartItemDeleted = new ShoppingCartItemDeleted(
            productCombinationId: productCombinationId,
            operationContext: operationContext);
        
        EnqueueEvent(shoppingCartItemDeleted);
        Apply(shoppingCartItemDeleted);
    }

    private void Apply(ShoppingCartItemDeleted @event)
    {
        Version++;

        var shoppingCartItem = _shoppingCartItems.FirstOrDefault(
            item => item.ProductCombinationId == @event.ProductCombinationId);
        if (shoppingCartItem is not null)
        {
            _shoppingCartItems.Remove(shoppingCartItem);
        }
    }
    
    public void Clear(IOperationContext operationContext)
    {
        Version++;

        var shoppingCartCleared = new ShoppingCartCleared(operationContext);
        
        EnqueueEvent(shoppingCartCleared);
        Apply(shoppingCartCleared);
    }

    private void Apply(ShoppingCartCleared shoppingCartCleared)
    {
        _shoppingCartItems.Clear();
    }
}