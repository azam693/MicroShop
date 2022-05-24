using Catalog.Application.Common.Models.Responses;
using Catalog.Application.Services.Products.CreateProduct;
using Catalog.Application.Services.Products.DeleteProduct;
using Catalog.Application.Services.Products.GetProductById;
using Catalog.Application.Services.Products.GetProducts;
using Catalog.Application.Services.Products.UpdateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ISender _mediatr;
    
    public ProductController(ISender mediatr)
    {
        _mediatr = mediatr;
    }
    
    /// <summary>
    /// Return product data by its id
    /// </summary>
    /// <param name="id">Product id</param>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ProductResponse> Get(Guid id)
    {
        return _mediatr.Send(new GetProductByIdQuery(id));
    }
    
    /// <summary>
    /// Return list of products
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    [HttpGet]
    [Route("select")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IEnumerable<ProductResponse>> Select(int pageNumber, int pageSize = 20)
    {
        return _mediatr.Send(new GetProductsQuery(pageNumber, pageSize));
    }
    
    /// <summary>
    /// Create new product
    /// </summary>
    /// <param name="product">Data of new product</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody]CreateProductCommand product)
    {
        var productId = await _mediatr.Send(product);

        return CreatedAtAction(nameof(Get), new { Id = productId }, product);
    }
    
    /// <summary>
    /// Update current product by id
    /// </summary>
    /// <param name="product">New data of updated product</param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody]UpdateProductCommand product)
    {
        await _mediatr.Send(product);

        return CreatedAtAction(nameof(Get), new { Id = product.Id }, product);
    }
    
    /// <summary>
    /// Delete product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediatr.Send(new DeleteProductCommand(id));

        return NoContent();
    }
}
