using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Categories;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService service) : BaseController
{
    private readonly ICategoryService _service = service;

    [HttpGet("GetAllCategories")]
    [ProducesResponseType(typeof(ResponseInfo<List<CategoryDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<CategoryDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<CategoryDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<CategoryDto>?>>> GetAllCategories(CancellationToken ct)
    {
        var result = await _service.GetAllCategories(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetCategories/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<CategoryDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<CategoryDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<CategoryDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<CategoryDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<CategoryDto>?>>> GetAllCategories(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<CategoryDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllCategories(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetCategory/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<CategoryDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<CategoryDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<CategoryDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<CategoryDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<CategoryDto?>>> GetCategory(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<CategoryDto?>.Failure("Invalid Category Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetCategoryById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchCategory/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<CategoryDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<CategoryDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<CategoryDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<CategoryDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<CategoryDto>?>>> SearchCategory(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<CategoryDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchCategory(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateCategory")]
    [ProducesResponseType(typeof(ResponseInfo<CategoryDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<CategoryDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<CategoryDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<CategoryDto?>>> CreateCategory([FromBody] CategoryDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<CategoryDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddCategory(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateCategory), new { id = result.Data?.CategoryId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateCategory")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateCategory([FromBody] CategoryDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateCategory(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteCategory/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteCategory(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Category Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteCategory(id, ct);
        return HandleResponse(result);
    }
}