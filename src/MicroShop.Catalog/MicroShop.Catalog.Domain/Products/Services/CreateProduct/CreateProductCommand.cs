using MicroShop.Catalog.Domain.Categories.Entities;
using MicroShop.Catalog.Domain.Common;
using MicroShop.Catalog.Domain.Products.Entities;

namespace MicroShop.Catalog.Domain.Products.Services.CreateProduct;

internal sealed class CreateProductCommand : ICreateProductCommand
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Category> _categoryRepository;

    public CreateProductCommand(
        IRepository<Product> productRepository,
        IRepository<Category> categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<string> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAsync(request.CategoryId, null, cancellationToken);
        if (category is null)
        {
            throw new CatalogException($"Category with id {request.CategoryId} doesn't exist");
        }

        var product = new Product(
            request.Name,
            request.Description,
            new ProductItem(
                request.Sku,
                request.Price,
                request.Attribute),
            new ProductCategory(category.Id, category.Name));

        await _productRepository.CreateAsync(product, product.Category.Name, cancellationToken);

        return product.Id;
    }
}
