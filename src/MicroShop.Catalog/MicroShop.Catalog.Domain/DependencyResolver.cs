using MicroShop.Catalog.Domain.Categories.Services.CreateCategory;
using MicroShop.Catalog.Domain.Categories.Services.DeleteCategory;
using MicroShop.Catalog.Domain.Categories.Services.GetCategory;
using MicroShop.Catalog.Domain.Products.Services.CreateProduct;
using MicroShop.Catalog.Domain.Products.Services.DeleteProduct;
using MicroShop.Catalog.Domain.Products.Services.GetProduct;
using MicroShop.Catalog.Domain.Products.Services.UpdateProduct;
using Microsoft.Extensions.DependencyInjection;

namespace MicroShop.Catalog.Domain;

public static class DependencyResolver
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddScoped<ICreateCategoryCommand, CreateCategoryCommand>();
        services.AddScoped<IDeleteCategoryCommand, DeleteCategoryCommand>();
        services.AddScoped<IGetCategoryQuery, GetCategoryQuery>();

        services.AddScoped<ICreateProductCommand, CreateProductCommand>();
        services.AddScoped<IUpdateProductCommand, UpdateProductCommand>();
        services.AddScoped<IDeleteProductCommand, DeleteProductCommand>();
        services.AddScoped<IGetProductQuery, GetProductQuery>();
    }
}
