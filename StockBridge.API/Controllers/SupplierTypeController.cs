using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.SupplierTypes;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierTypeController(ISupplierTypeService service) : BaseController
{
    private readonly ISupplierTypeService _service = service;

    [HttpGet("GetAllSupplierTypes")]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierTypeDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierTypeDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierTypeDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<SupplierTypeDto>?>>> GetAllSupplierTypes(CancellationToken ct)
    {
        var result = await _service.GetAllSupplierTypes(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetSupplierTypes/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierTypeDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierTypeDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierTypeDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierTypeDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<SupplierTypeDto>?>>> GetAllSupplierTypes(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<SupplierTypeDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllSupplierTypes(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetSupplierType/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<SupplierTypeDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<SupplierTypeDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<SupplierTypeDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<SupplierTypeDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<SupplierTypeDto?>>> GetSupplierType(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<SupplierTypeDto?>.Failure("Invalid SupplierType Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetSupplierTypeById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchSupplierType/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierTypeDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierTypeDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierTypeDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierTypeDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<SupplierTypeDto>?>>> SearchSupplierType(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<SupplierTypeDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchSupplierType(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateSupplierType")]
    [ProducesResponseType(typeof(ResponseInfo<SupplierTypeDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<SupplierTypeDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<SupplierTypeDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<SupplierTypeDto?>>> CreateSupplierType([FromBody] SupplierTypeDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<SupplierTypeDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddSupplierType(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateSupplierType), new { id = result.Data?.SupplierTypeId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateSupplierType")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateSupplierType([FromBody] SupplierTypeDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateSupplierType(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteSupplierType/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteSupplierType(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid SupplierType Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteSupplierType(id, ct);
        return HandleResponse(result);
    }
}
