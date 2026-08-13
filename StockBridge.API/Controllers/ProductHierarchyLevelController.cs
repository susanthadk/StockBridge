using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.ProductHierarchyLevels;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductHierarchyLevelController(IProductHierarchyLevelService service) : BaseController
{
    private readonly IProductHierarchyLevelService _service = service;

    [HttpGet("GetAllProductHierarchyLevels")]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyLevelDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyLevelDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyLevelDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<ProductHierarchyLevelDto>?>>> GetAllProductHierarchyLevels(CancellationToken ct)
    {
        var result = await _service.GetAllProductHierarchyLevels(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetProductHierarchyLevels/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyLevelDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyLevelDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyLevelDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyLevelDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<ProductHierarchyLevelDto>?>>> GetAllProductHierarchyLevels(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<ProductHierarchyLevelDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllProductHierarchyLevels(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetProductHierarchyLevel/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyLevelDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyLevelDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyLevelDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyLevelDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<ProductHierarchyLevelDto?>>> GetProductHierarchyLevel(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<ProductHierarchyLevelDto?>.Failure("Invalid ProductHierarchyLevel Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetProductHierarchyLevelById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchProductHierarchyLevel/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyLevelDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyLevelDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyLevelDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyLevelDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<ProductHierarchyLevelDto>?>>> SearchProductHierarchyLevel(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<ProductHierarchyLevelDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchProductHierarchyLevel(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateProductHierarchyLevel")]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyLevelDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyLevelDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyLevelDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<ProductHierarchyLevelDto?>>> CreateProductHierarchyLevel([FromBody] ProductHierarchyLevelDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<ProductHierarchyLevelDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddProductHierarchyLevel(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateProductHierarchyLevel), new { id = result.Data?.ProductHierarchyLevelId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateProductHierarchyLevel")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateProductHierarchyLevel([FromBody] ProductHierarchyLevelDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateProductHierarchyLevel(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteProductHierarchyLevel/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteProductHierarchyLevel(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid ProductHierarchyLevel Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteProductHierarchyLevel(id, ct);
        return HandleResponse(result);
    }
}