using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.ProductHierarchies;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductHierarchyController(IProductHierarchyService service) : BaseController
{
    private readonly IProductHierarchyService _service = service;

    [HttpGet("GetAllProductHierarchies")]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<ProductHierarchyDto>?>>> GetAllProductHierarchies(CancellationToken ct)
    {
        var result = await _service.GetAllProductHierarchies(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetProductHierarchies/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<ProductHierarchyDto>?>>> GetAllProductHierarchies(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<ProductHierarchyDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllProductHierarchies(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetProductHierarchy/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<ProductHierarchyDto?>>> GetProductHierarchy(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<ProductHierarchyDto?>.Failure("Invalid ProductHierarchy Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetProductHierarchyById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchProductHierarchy/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<ProductHierarchyDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<ProductHierarchyDto>?>>> SearchProductHierarchy(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<ProductHierarchyDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchProductHierarchy(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateProductHierarchy")]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<ProductHierarchyDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<ProductHierarchyDto?>>> CreateProductHierarchy([FromBody] ProductHierarchyDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<ProductHierarchyDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddProductHierarchy(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateProductHierarchy), new { id = result.Data?.ProductHierarchyId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateProductHierarchy")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateProductHierarchy([FromBody] ProductHierarchyDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateProductHierarchy(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteProductHierarchy/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteProductHierarchy(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid ProductHierarchy Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteProductHierarchy(id, ct);
        return HandleResponse(result);
    }
}