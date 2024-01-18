using MicroShop.Catalog.Domain.Common;
using MicroShop.Catalog.Domain.Products.Entities;

namespace MicroShop.Catalog.Domain.Products.Services.DeleteProduct;

internal sealed class DeleteProductCommand : IDeleteProductCommand
{
    private IRepository<Product> _productRepository;

    public DeleteProductCommand(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteAsync(
            request.Id,
            request.CategoryName,
            cancellationToken);
    }
}
