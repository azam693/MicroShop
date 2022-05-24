using Catalog.Application.Common.Models.Responses;
using Catalog.Application.Services.Categories.CreateCategory;
using Catalog.Application.Services.Categories.DeleteCategory;
using Catalog.Application.Services.Categories.GetCategories;
using Catalog.Application.Services.Categories.GetCategoryById;
using Catalog.Application.Services.Categories.UpdateCategory;
using Catalog.Application.Services.Products.CreateProduct;
using Catalog.Application.Services.Products.DeleteProduct;
using Catalog.Application.Services.Products.UpdateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ISender _mediatr;
    
    public CategoryController(ISender mediatr)
    {
        _mediatr = mediatr;
    }
    
    /// <summary>
    /// Return category data by its id
    /// </summary>
    /// <param name="id">Product id</param>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<CategoryResponse> Get(Guid id)
    {
        return _mediatr.Send(new GetCategoryByIdQuery(id));
    }
    
    /// <summary>
    /// Return list of categories
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    [HttpGet]
    [Route("select")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IEnumerable<CategoryResponse>> Select(int pageNumber, int pageSize = 20)
    {
        return _mediatr.Send(new GetCategoriesQuery(pageNumber, pageSize));
    }
    
    /// <summary>
    /// Create new category
    /// </summary>
    /// <param name="category">Data of new category</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody]CreateCategoryCommand category)
    {
        var categoryId = await _mediatr.Send(category);

        return CreatedAtAction(nameof(Get), new { Id = categoryId }, category);
    }
    
    /// <summary>
    /// Update current category by id
    /// </summary>
    /// <param name="category">New data of updated category</param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody]UpdateCategoryCommand category)
    {
        await _mediatr.Send(category);

        return CreatedAtAction(nameof(Get), new { Id = category.Id }, category);
    }
    
    /// <summary>
    /// Delete category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediatr.Send(new DeleteCategoryCommand(id));

        return NoContent();
    }
}