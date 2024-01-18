using MicroShop.Catalog.Domain.Common;
using MicroShop.Catalog.Domain.Products.Entities;

namespace MicroShop.Catalog.Domain.Products.Services.GetProduct;

internal sealed class GetProductQuery : IGetProductQuery
{
    private readonly IRepository<Product> _productRepository;

    public GetProductQuery(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetProductResponse> Handle(GetProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(
            request.Id, 
            request.CategoryId, 
            cancellationToken);

        return GetProductResponse.Create(product);
    }
}
